import * as fs from "fs";
import * as _ from "lodash";
import * as consola from "consola";

var time = new Date();

let day = "05";

// let input = fs.readFileSync("src/day" + day + "/_test.txt", {
//   encoding: "utf8",
// });
let input = fs.readFileSync("src/day" + day + "/_input.txt", {
  encoding: "utf8",
});

let rows = input.split("\r\n");
let answers: number[] = [];

for (let row of rows) {
  let rowParts = Array.from(row.substring(0, 7));

  let rowMin = 0;
  let rowMax = 127;

  for (let rowPart of rowParts) {
    let rowMiddle = (rowMax - rowMin) / 2;

    switch (rowPart) {
      case "F":
        rowMax -= rowMiddle;

        rowMax = Math.floor(rowMax);
        break;
      case "B":
        rowMin += rowMiddle;

        rowMin = Math.ceil(rowMin);
        break;
    }
  }

  let rowValue = rowMin;

  let columnParts = Array.from(row.substring(7));

  let columnMin = 0;
  let columnMax = 7;

  for (let columnPart of columnParts) {
    let columnMiddle = (columnMax - columnMin) / 2;

    switch (columnPart) {
      case "L":
        columnMax -= columnMiddle;

        columnMax = Math.floor(columnMax);
        break;
      case "R":
        columnMin += columnMiddle;

        columnMin = Math.ceil(columnMin);
        break;
    }
  }

  let columnValue = columnMin;

  answers.push(rowValue * 8 + columnValue);
}

let answer = _.max(answers);

consola.default.info("Answer: " + answer);

let validAnswer = 926;

validAnswer
  ? answer === validAnswer
    ? consola.default.success("Valid")
    : consola.default.error("Not valid anymore, answer must be " + validAnswer)
  : consola.default.warn("No valid answer known yet");

let executionTime = new Date().getTime() - time.getTime();

console.info(executionTime + "ms");
console.log();

process.send(executionTime);
