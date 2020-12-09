import * as _ from "lodash";
import { endExecution, readAsLines, startExecution } from "../helpers";

let time = startExecution();

let day = "09";

//let lines = readAsLines("test", day);
let lines = readAsLines("input", day);

let numbers = lines.map((x) => parseInt(x, 10));

let firstLines = numbers.splice(0, 3);

let preamble = firstLines[0];

let answer = null;

for (let i = preamble; i < numbers.length; i++) {
  let section = numbers.slice(i - preamble, i);

  let wish = numbers[i];

  let containsSum = false;

  for (let j = 0; j < section.length; j++) {
    for (let k = 0; k < section.length; k++) {
      if (j !== k) {
        if (section[j] + section[k] === wish) {
          containsSum = true;

          break;
        }
      }
    }

    if (containsSum) {
      break;
    }
  }

  if (!containsSum) {
    answer = wish;

    break;
  }
}

endExecution(time, answer, 90433990);
