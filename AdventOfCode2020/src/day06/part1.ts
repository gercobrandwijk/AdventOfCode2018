import * as fs from "fs";
import * as _ from "lodash";
import * as consola from "consola";

var time = new Date();

let day = "06";

// let input = fs.readFileSync("src/day" + day + "/_test.txt", {
//   encoding: "utf8",
// });
let input = fs.readFileSync("src/day" + day + "/_input.txt", {
  encoding: "utf8",
});

let answer: number = input
  .split("\r\n\r\n")
  .map((group) => {
    return group.split("\r\n");
  })
  .map((groupRows) => {
    let groupAnswer: Set<string> = new Set();

    groupRows.forEach((groupRow) =>
      Array.from(groupRow).forEach((character) => groupAnswer.add(character))
    );

    return groupAnswer.size;
  })
  .reduce((sum, size) => sum + size, 0);

consola.default.info("Answer: " + answer);

let validAnswer = 6310;

validAnswer
  ? answer === validAnswer
    ? consola.default.success("Valid")
    : consola.default.error("Not valid anymore, answer must be " + validAnswer)
  : consola.default.warn("No valid answer known yet");

let executionTime = new Date().getTime() - time.getTime();

console.info(executionTime + "ms");
console.log();

process.send(executionTime);
