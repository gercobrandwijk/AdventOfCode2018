import * as fs from "fs";
import * as _ from "lodash";
import * as consola from "consola";

//let input = fs.readFileSync("src/day02/_test.txt", { encoding: "utf8" });
let input = fs.readFileSync("src/day02/_input.txt", { encoding: "utf8" });

let rows = input.split("\n").map((x) => {
  let parts = x.split(":");

  let ruleParts = parts[0].split(" ");

  return {
    character: ruleParts[1],
    min: parseInt(ruleParts[0].split("-")[0], 10),
    max: parseInt(ruleParts[0].split("-")[1], 10),
    value: parts[1].trim(),
  };
});

let answer = 0;

for (let row of rows) {
  let characterAmount = row.value.split("").filter((x) => x === row.character)
    .length;

  if (characterAmount >= row.min && characterAmount <= row.max)
    answer++;
}

consola.default.info("Answer: " + answer);

let validAnswer = 445;

validAnswer
  ? answer === validAnswer
    ? consola.default.success("Valid")
    : consola.default.error("Not valid anymore, answer must be " + validAnswer)
  : consola.default.warn("No valid answer known yet");
