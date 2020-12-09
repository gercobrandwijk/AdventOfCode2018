import * as fs from "fs";
import * as consola from "consola";

export function startExecution() {
  return new Date().getTime();
}

export function read(type: "test" | "input", day: string) {
  return fs.readFileSync("src/day" + day + "/_" + type + ".txt", {
    encoding: "utf8",
  });
}

export function readAsLines(type: "test" | "input", day: string, seperator: string = "\r\n") {
  return read(type, day).split(seperator);
}

export function endExecution(time: number, answer: any, validAnswer: any) {
  consola.default.info("Answer: " + answer);

  validAnswer
    ? answer === validAnswer
      ? consola.default.success("Valid")
      : consola.default.error(
          "Not valid anymore, answer must be " + validAnswer
        )
    : consola.default.warn("No valid answer known yet");

  let executionTime = new Date().getTime() - time;

  console.info(executionTime + "ms");
  console.log();

  process.send(executionTime);
}
