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

let groups: string[] = input.split("\r\n\r\n");

let answer = 0;

for (let group of groups) {
  let groupAnswer = [];

  let groupRows: string[] = group.split("\r\n");

  let groupRowItems = groupRows.map((x) => Array.from(x));

  for (let groupRowItem of groupRowItems) {
    for (let groupRowItemCharacter of groupRowItem) {
      groupAnswer.push(groupRowItemCharacter);
    }
  }

  groupAnswer = _.uniq(groupAnswer);

  answer += groupAnswer.length;
}

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
