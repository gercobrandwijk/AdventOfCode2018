import * as _ from "lodash";
import { endExecution, readAsLines, startExecution } from "../helpers";

let time = startExecution();

let day = "10";

//let lines = readAsLines("test1", day);
//let lines = readAsLines("test2", day);
let lines = readAsLines("input", day);

let numbers = lines.map((x) => parseInt(x, 10));
numbers.sort((x, y) => x - y);

numbers.unshift(0);
numbers.push(numbers[numbers.length - 1] + 3);

let jolt1Differences = 0;
let jolt2Differences = 0;
let jolt3Differences = 0;

let currentNumber = 0;

for (let i = 0; i < numbers.length; i++) {
  if (numbers[i] === currentNumber + 1) {
    jolt1Differences++;
  } else if (numbers[i] === currentNumber + 2) {
    jolt2Differences++;
  } else if (numbers[i] === currentNumber + 3) {
    jolt3Differences++;
  }

  currentNumber = numbers[i];
}

let answer = jolt1Differences * jolt3Differences;

endExecution(time, answer, 2046);
