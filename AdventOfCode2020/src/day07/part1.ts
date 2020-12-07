import * as fs from "fs";
import * as _ from "lodash";
import * as consola from "consola";

var time = new Date();

let day = "07";

// let input = fs.readFileSync("src/day" + day + "/_test.txt", {
//   encoding: "utf8",
// });
let input = fs.readFileSync("src/day" + day + "/_input.txt", {
  encoding: "utf8",
});

class Bag {
  parents: string[];
  children: { [key: string]: number };
}

let bagDefinitions: { [key: string]: Bag } = {};

let bagRules = input.split("\r\n");

for (let bagRule of bagRules) {
  let bagRuleSplit = bagRule.split(" bags contain ");

  let currentBag = bagRuleSplit[0];

  if (!bagDefinitions[currentBag]) {
    bagDefinitions[currentBag] = { parents: [], children: {} };
  }

  let bagRuleSplitChildren = bagRuleSplit[1].split(", ");

  for (let bagRuleChild of bagRuleSplitChildren) {
    let bagRuleChildSplit = bagRuleChild.split(" bag")[0].split(" ");

    let childName = bagRuleChildSplit
      .filter((_, index) => index !== 0)
      .join(" ");

    if (childName !== "other") {
      bagDefinitions[currentBag].children[childName] = parseInt(
        bagRuleChildSplit[0],
        10
      );

      if (!bagDefinitions[childName]) {
        bagDefinitions[childName] = { parents: [], children: {} };
      }

      bagDefinitions[childName].parents.push(currentBag);
    }
  }
}

let answer = 0;
let parents: { [key: string]: string } = {};

function findParents(item: Bag) {
  for (let parent of item.parents) {
    if (parents[parent]) {
      continue;
    }

    answer++;

    findParents(bagDefinitions[parent]);

    parents[parent] = "filled";
  }
}

findParents(bagDefinitions["shiny gold"]);

consola.default.info("Answer: " + answer);

let validAnswer = 252;

validAnswer
  ? answer === validAnswer
    ? consola.default.success("Valid")
    : consola.default.error("Not valid anymore, answer must be " + validAnswer)
  : consola.default.warn("No valid answer known yet");

let executionTime = new Date().getTime() - time.getTime();

console.info(executionTime + "ms");
console.log();

process.send(executionTime);
