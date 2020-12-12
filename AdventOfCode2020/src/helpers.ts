import * as fs from "fs";
import * as consola from "consola";
import { Execution } from "./models";

export function start(
  executions: Execution[],
  useTestFile: boolean | "1" | "2" = false
) {
  let result = {
    time: new Date().getTime(),
    execution: executions.find((x) => {
      if (useTestFile === true) return x.file === "test";
      if (useTestFile === "1") return x.file === "test1";
      if (useTestFile === "2") return x.file === "test2";

      return x.file === "input";
    }),
  };

  if (!result.execution) {
    throw new Error("Execution not found");
  }

  return result;
}

export function end(time: number, answer: any, execution: Execution) {
  if (execution.answer) {
    if (answer === execution.answer) {
      consola.default.success("Valid");
    } else {
      consola.default.error("Invalid, must be " + execution.answer);
    }
  }

  consola.default.info("Answer   " + answer);

  let executionTime = new Date().getTime() - time;

  consola.default.info("Time     " + executionTime + "ms");

  console.log();

  process.send(executionTime);
}

export function read(day: string, execution: Execution) {
  return fs.readFileSync("src/day" + day + "/_" + execution.file + ".txt", {
    encoding: "utf8",
  });
}

export function readAsLines(
  day: string,
  execution: Execution,
  seperator: string = "\r\n"
) {
  return read(day, execution).split(seperator);
}

export function writeFile(day: string, name: string, data: string) {
  fs.writeFileSync("dist/day" + day + "/output_" + name + ".txt", data);
}
