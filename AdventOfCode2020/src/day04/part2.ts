import * as fs from "fs";
import * as _ from "lodash";
import * as consola from "consola";
import { isNumber } from "lodash";

var time = new Date();

let day = "04";

// let input = fs.readFileSync("src/day" + day + "/_test.txt", {
//   encoding: "utf8",
// });
let input = fs.readFileSync("src/day" + day + "/_input.txt", {
  encoding: "utf8",
});
// let input = fs.readFileSync("src/day" + day + "/_input_part2_valid.txt", {
//   encoding: "utf8",
// });
// let input = fs.readFileSync("src/day" + day + "/_input_part2_invalid.txt", {
//   encoding: "utf8",
// });

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

  if (parseInt(x.byr, 10) < 1920 || parseInt(x.byr) > 2002) {
    return false;
  }

  if (parseInt(x.iyr, 10) < 2010 || parseInt(x.iyr) > 2020) {
    return false;
  }

  if (parseInt(x.eyr, 10) < 2020 || parseInt(x.eyr) > 2030) {
    return false;
  }

  if (x.hgt.indexOf("cm") >= 0) {
    let height = parseInt(x.hgt.substring(0, x.hgt.length - 2));

    if (height < 150 || height > 193) {
      return false;
    }
  } else if (x.hgt.indexOf("in") >= 0) {
    let height = parseInt(x.hgt.substring(0, x.hgt.length - 2));

    if (height < 59 || height > 76) {
      return false;
    }
  } else {
    return false;
  }

  if (x.hcl.length !== 7 || x.hcl.indexOf("#") !== 0) {
    return false;
  } else if (!x.hcl.substring(1).match(/^[a-f0-9]+$/i)) {
    return false;
  }

  if (["amb", "blu", "brn", "gry", "grn", "hzl", "oth"].indexOf(x.ecl) < 0) {
    return false;
  }

  if (x.pid.length !== 9 || !isNumber(parseInt(x.pid))) {
    return false;
  }

  return true;
});

let answer = validPasswords.length;

consola.default.info("Answer: " + answer);

let validAnswer = 198;

validAnswer
  ? answer === validAnswer
    ? consola.default.success("Valid")
    : consola.default.error("Not valid anymore, answer must be " + validAnswer)
  : consola.default.warn("No valid answer known yet");

let executionTime = new Date().getTime() - time.getTime();

console.info(executionTime + "ms");
console.log();

process.send(executionTime);
