import * as fs from "fs";
import * as _ from "lodash";
import * as consola from "consola";

var time = new Date();

let day = "01";

// let input = fs.readFileSync("src/day" + day + "/_test.txt", {
//   encoding: "utf8",
// });
let input = fs.readFileSync("src/day" + day + "/_input.txt", {
  encoding: "utf8",
});

let numbers = input.split("\n").map((x) => parseInt(x, 10));

function calculate() {
  let first;
  let second;

  for (let i = 0; i < numbers.length; i++) {
    first = numbers[i];

    for (let j = 0; j < numbers.length; j++) {
      second = numbers[j];

      if (first + second === 2020) {
        return first * second;
      }
    }
  }
}

let answer = calculate();

consola.default.info("Answer: " + answer);

let validAnswer = 1006875;

validAnswer
  ? answer === validAnswer
    ? consola.default.success("Valid")
    : consola.default.error("Not valid anymore, answer must be " + validAnswer)
  : consola.default.warn("No valid answer known yet");

let executionTime = new Date().getTime() - time.getTime();

console.info(executionTime + "ms");
console.log();

process.send(executionTime);
