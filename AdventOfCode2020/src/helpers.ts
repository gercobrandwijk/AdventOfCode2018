import * as fs from "fs";
import * as consola from "consola";

export function startExecution() {
  return new Date().getTime();
}

export function read(type: "test" | "test1" | "test2" | "input", day: string) {
  return fs.readFileSync("src/day" + day + "/_" + type + ".txt", {
    encoding: "utf8",
  });
}

export function readAsLines(
  type: "test" | "test1" | "test2" | "input",
  day: string,
  seperator: string = "\r\n"
) {
  return read(type, day).split(seperator);
}

export function writeArray(day: string, name: string, data: any[]) {
  fs.writeFileSync(
    "dist/day" + day + "/output_" + name + ".txt",
    data.join("\r\n")
  );
}

export function endExecution(time: number, answer: any, validAnswer: any) {
  if (validAnswer) {
    if (answer === validAnswer) {
      consola.default.success("Valid");
    } else {
      consola.default.error("Invalid, must be " + validAnswer);
    }
  }

  consola.default.info("Answer   " + answer);

  let executionTime = new Date().getTime() - time;

  consola.default.info("Time     " + executionTime + "ms");

  console.log();

  process.send(executionTime);
}
