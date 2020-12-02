import * as fs from "fs";
import * as path from "path";
import * as childProcess from "child_process";

const directoryPath = path.join(__dirname);

function runScript(scriptPath, callback) {
  // keep track of whether callback has been invoked to prevent multiple invocations
  var invoked = false;

  var childProcessFork = childProcess.fork(scriptPath);

  var time = new Date();

  function done(err) {
    console.info(new Date().getTime() - time.getTime() + "ms");
    console.log();

    callback(err);
  }

  // listen for errors as they may prevent the exit event from firing
  childProcessFork.on("error", function (err) {
    if (invoked) return;
    invoked = true;

    done(err);
  });

  // execute the callback once the process has finished running
  childProcessFork.on("exit", function (code) {
    if (invoked) return;
    invoked = true;
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
          if (err) console.error(err);

          resolve();
        });
      });
    });
  }
}
