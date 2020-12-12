import * as _ from "lodash";
import { end, readAsLines, start } from "../helpers";

let { time, execution } = start([
  { file: "test1", answer: 8 },
  { file: "test2", answer: 19208 },
  { file: "input", answer: 2046 },
]);

let lines = readAsLines("10", execution);

let numbers = lines.map((x) => parseInt(x, 10));
numbers.sort((x, y) => x - y);

numbers.unshift(0);
numbers.push(numbers[numbers.length - 1] + 3);

let jolt1Differences = 0;
let jolt3Differences = 0;

let lastNumber = 0;

for (let i = 1; i < numbers.length; i++) {
  if (numbers[i] === lastNumber + 1) {
    jolt1Differences++;
  } else if (numbers[i] === lastNumber + 3) {
    jolt3Differences++;
  }

  lastNumber = numbers[i];
}

let answer = jolt1Differences * jolt3Differences;

end(time, answer, execution);
