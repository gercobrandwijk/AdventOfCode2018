import * as fs from "fs";
import * as _ from "lodash";

//let input = fs.readFileSync("src/day02/_test.txt", { encoding: "utf8" });
let input = fs.readFileSync("src/day02/_input.txt", { encoding: "utf8" });

let rows = input.split("\n").map((x) => {
  let parts = x.split(":");

  let ruleParts = parts[0].split(" ");

  return {
    character: ruleParts[1],
    pos1: parseInt(ruleParts[0].split("-")[0], 10),
    pos2: parseInt(ruleParts[0].split("-")[1], 10),
    value: parts[1].trim(),
  };
});

let passwordValidCount = 0;

for (let row of rows) {
  let characters = row.value.split("");

  if (characters[row.pos1 - 1] === row.character) {
    if (characters[row.pos2 - 1] !== row.character) passwordValidCount++;
  } else if (characters[row.pos2 - 1] === row.character) {
    if (characters[row.pos1 - 1] !== row.character) passwordValidCount++;
  }
}

console.log(passwordValidCount);
