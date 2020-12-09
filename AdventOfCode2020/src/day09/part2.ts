import * as _ from "lodash";
import { endExecution, readAsLines, startExecution } from "../helpers";

let time = startExecution();

let day = "09";

//let lines = readAsLines("test", day);
let lines = readAsLines("input", day);

let numbers = lines.map((x) => parseInt(x, 10));

let firstLines = numbers.splice(0, 3);

let invalidNumber = firstLines[1];
let invalidNumberIndex = numbers.indexOf(invalidNumber);

let answer = null;

for (let startNumber = 0; startNumber < numbers.length; startNumber++) {
  if (startNumber === invalidNumberIndex) continue;

  let section;

  if (startNumber < invalidNumberIndex) {
    section = numbers.slice(startNumber, invalidNumberIndex);
  } else {
    section = numbers.slice(startNumber, numbers.length);
  }

  let sum = 0;
  let lowestValue = null;
  let highestValue = null;

  for (let j = 0; j < section.length; j++) {
    sum += section[j];

    if (lowestValue === null || section[j] < lowestValue) {
      lowestValue = section[j];
    }

    if (highestValue === null || section[j] > highestValue) {
      highestValue = section[j];
    }

    if (sum === invalidNumber) {
      break;
    }
  }

  if (sum === invalidNumber) {
    answer = lowestValue + highestValue;

    break;
  }
}

endExecution(time, answer, 11691646);
