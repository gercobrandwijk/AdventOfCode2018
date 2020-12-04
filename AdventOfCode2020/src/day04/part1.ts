import * as fs from "fs";
import * as _ from "lodash";
import * as consola from "consola";

var time = new Date();

let day = "04";

// let input = fs.readFileSync("src/day" + day + "/_test.txt", {
//   encoding: "utf8",
// });
let input = fs.readFileSync("src/day" + day + "/_input.txt", {
  encoding: "utf8",
});

let items = input.split("\r\n\r\n");

items = items.map((item) => {
  return item.replace(/(?:\r\n|\r|\n)/g, " ");
});

let passports: { [key: string]: string }[] = items.map((x) => {
  let entryItems = x.split(" ");

  return entryItems
    .map((x) => {
      return { key: x.split(":")[0], value: x.split(":")[1] };
    })
    .reduce((a, x) => ({ ...a, [x.key]: x.value }), {});
});

let validPasswords = passports.filter((x) => {
  delete x.cid;

  let keys = Object.keys(x);

  if (keys.length !== 7) {
    return false;
  }

  return true;
});

let answer = validPasswords.length;

consola.default.info("Answer: " + answer);

let validAnswer = 256;

validAnswer
  ? answer === validAnswer
    ? consola.default.success("Valid")
    : consola.default.error("Not valid anymore, answer must be " + validAnswer)
  : consola.default.warn("No valid answer known yet");

let executionTime = new Date().getTime() - time.getTime();

console.info(executionTime + "ms");
console.log();

process.send(executionTime);
