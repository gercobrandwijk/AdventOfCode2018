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
  name: string;
  parents: string[];
  children: { name: string; amount: number }[];
  count?: number;
}

let bagDefinitions: { [key: string]: Bag } = {};

let bagRules = input.split("\r\n");

for (let bagRule of bagRules) {
  let bagRuleSplit = bagRule.split(" bags contain ");

  let currentBag = bagRuleSplit[0];

  if (!bagDefinitions[currentBag]) {
    bagDefinitions[currentBag] = {
      name: currentBag,
      parents: [],
      children: [],
    };
  }

  let bagRuleSplitChildren = bagRuleSplit[1].split(", ");

  for (let bagRuleChild of bagRuleSplitChildren) {
    let bagRuleChildSplit = bagRuleChild.split(" bag")[0].split(" ");

    let childName = bagRuleChildSplit
      .filter((_, index) => index !== 0)
      .join(" ");

    if (childName !== "other") {
      bagDefinitions[currentBag].children.push({
        name: childName,
        amount: parseInt(bagRuleChildSplit[0], 10),
      });

      if (!bagDefinitions[childName]) {
        bagDefinitions[childName] = {
          name: childName,
          parents: [],
          children: [],
        };
      }

      bagDefinitions[childName].parents.push(currentBag);
    }
  }
}

function calculateCount(item: Bag) {
  if (item.count !== undefined && item.count !== null) return;

  item.count = 0;

  for (let child of item.children) {
    if (
      bagDefinitions[child.name].count === undefined ||
      bagDefinitions[child.name].count === null
    ) {
      calculateCount(bagDefinitions[child.name]);
    }

    item.count +=
      child.amount + child.amount * bagDefinitions[child.name].count;
  }
}

calculateCount(bagDefinitions["shiny gold"]);

let answer = bagDefinitions["shiny gold"].count;

consola.default.info("Answer: " + answer);

let validAnswer = 35487;

validAnswer
  ? answer === validAnswer
    ? consola.default.success("Valid")
    : consola.default.error("Not valid anymore, answer must be " + validAnswer)
  : consola.default.warn("No valid answer known yet");

let executionTime = new Date().getTime() - time.getTime();

console.info(executionTime + "ms");
console.log();

process.send(executionTime);
