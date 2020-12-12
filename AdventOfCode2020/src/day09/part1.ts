import * as _ from "lodash";
import { end, readAsLines, start } from "../helpers";

let { time, execution } = start([
  { file: "test", answer: 127 },
  { file: "input", answer: 90433990 },
]);

let lines = readAsLines("09", execution);

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

end(time, answer, execution);
