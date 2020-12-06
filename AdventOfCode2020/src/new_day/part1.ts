import * as fs from "fs";
import * as _ from "lodash";
import * as consola from "consola";

var time = new Date();

let day = "XX";

let input = fs.readFileSync("src/day" + day + "/_test.txt", {
  encoding: "utf8",
});
// let input = fs.readFileSync("src/day" + day + "/_input.txt", {
//   encoding: "utf8",
// });












let answer = undefined;

consola.default.info("Answer: " + answer);

let validAnswer = undefined;

validAnswer
  ? answer === validAnswer
    ? consola.default.success("Valid")
    : consola.default.error("Not valid anymore, answer must be " + validAnswer)
  : consola.default.warn("No valid answer known yet");

let executionTime = new Date().getTime() - time.getTime();

console.info(executionTime + "ms");
console.log();

process.send(executionTime);