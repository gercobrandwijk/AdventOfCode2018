import * as fs from "fs";
import * as _ from "lodash";
import * as consola from "consola";

var time = new Date();

let day = "03";

// let input = fs.readFileSync("src/day" + day + "/_test.txt", {
//   encoding: "utf8",
// });
let input = fs.readFileSync("src/day" + day + "/_input.txt", {
  encoding: "utf8",
});

let map = input.split("\r\n").map((x) => x.split(""));

let endRow = map.length;

let answer = 0;

let currentX = 0;
let currentY = 0;

let step = { x: 3, y: 1 };

while (true) {
  currentX += step.x;
  currentY += step.y;

  if (currentY >= endRow) {
    break;
  }

  if (currentX > map[0].length - 1) {
    currentX -= map[0].length;
  }

  if (map[currentY][currentX] === "#") {
    answer++;

    map[currentY][currentX] = "X";
  } else {
    map[currentY][currentX] = "O";
  }
}

consola.default.info("Answer: " + answer);

let validAnswer = 156;

validAnswer
  ? answer === validAnswer
    ? consola.default.success("Valid")
    : consola.default.error("Not valid anymore, answer must be " + validAnswer)
  : consola.default.warn("No valid answer known yet");

let executionTime = new Date().getTime() - time.getTime();

console.info(executionTime + "ms");
console.log();

process.send(executionTime);
