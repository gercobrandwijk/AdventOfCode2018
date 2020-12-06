import * as fs from "fs";
import * as path from "path";
import * as childProcess from "child_process";

const directoryPath = path.join(__dirname);

let executionTime = 0;

function runScript(scriptPath, callback) {
  var callbackReceived = false;

  var childProcessFork = childProcess.fork(scriptPath);

  function done(err) {
    callback(err);
  }

  childProcessFork.on("message", function (message) {
    executionTime += parseInt(message.toString(), 10);
  });

  childProcessFork.on("error", function (err) {
    if (callbackReceived) {
      return;
    }

    callbackReceived = true;

    done(err);
  });

  childProcessFork.on("exit", function (code) {
    if (callbackReceived) {
      return;
    }

    callbackReceived = true;

    var err = code === 0 ? null : new Error("exit code " + code);

    done(err);
  });
}

let days = fs
  .readdirSync(directoryPath)
  .filter((x) =>
    process.argv[2] !== "today"
      ? x.startsWith("day")
      : x === "day" + ("0" + new Date().getDate()).slice(-2)
  );

let promiseChain = Promise.resolve();

for (let day of days) {
  let paths = ["dist/" + day + "/part1.js", "dist/" + day + "/part2.js"];

  for (let path of paths) {
    promiseChain = promiseChain.then(() => {
      return new Promise((resolve) => {
        console.log(path.substring(path.indexOf("/")));

        runScript(path, function (err) {
          if (err) {
            console.error(err);
          }

          resolve();
        });
      });
    });
  }

  promiseChain = promiseChain.then(() => {
    console.info("Total execution time: " + executionTime + "ms");
    console.log();
  });
}
