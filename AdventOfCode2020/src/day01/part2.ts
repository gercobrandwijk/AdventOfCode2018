import * as fs from "fs";
import * as _ from "lodash";

//let input = fs.readFileSync("src/day01/_test.txt", { encoding: "utf8" });
let input = fs.readFileSync("src/day01/_input.txt", { encoding: "utf8" });

let numbers = input.split("\n").map((x) => parseInt(x, 10));

function calculate() {
  let first;
  let second;
  let third;

  for (let i = 0; i < numbers.length; i++) {
    first = numbers[i];

    for (let j = 0; j < numbers.length; j++) {
      second = numbers[j];

      for (let k = 0; k < numbers.length; k++) {
        third = numbers[k];

        if (first + second + third === 2020) {
          return first * second * third;
        }
      }
    }
  }
}

console.log(calculate());
