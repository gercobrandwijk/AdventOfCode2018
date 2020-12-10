import * as _ from "lodash";
import { replace } from "lodash";
import {
  endExecution,
  readAsLines,
  startExecution,
  writeArray,
} from "../helpers";

let time = startExecution();

let day = "10";

//let lines = readAsLines("test1", day);
//let lines = readAsLines("test2", day);
let lines = readAsLines("input", day);

let numbers = lines.map((x) => parseInt(x, 10));
numbers.sort((x, y) => x - y);

numbers.unshift(0);
numbers.push(numbers[numbers.length - 1] + 3);

class Graph {
  index: number;
  value: number;

  pathCount?: number;

  children: Graph[];
}

let graphs: { [index: number]: Graph } = {};

for (let i = 0; i < numbers.length; i++) {
  if (!graphs[i]) {
    graphs[i] = {
      index: i,
      value: numbers[i],
      children: [],
    };
  }

  for (let j = i + 1; j < numbers.length; j++) {
    if (numbers[j] >= numbers[i] + 1 && numbers[j] <= numbers[i] + 3) {
      if (!graphs[j]) {
        graphs[j] = {
          index: j,
          value: numbers[j],
          children: [],
        };
      }

      graphs[i].children.push(graphs[j]);
    }

    if (numbers[j] >= numbers[i] + 3) {
      break;
    }
  }
}

function calculateGraphs(from: number, to: number): number {
  let pathCount = 0;

  let currentGraph = graphs[from];

  if (from === to) {
    pathCount++;

    currentGraph.pathCount = pathCount;
  } else {
    for (let next of currentGraph.children.filter(
      (x) => x.pathCount === undefined || x.pathCount === null
    )) {
      calculateGraphs(next.index, to);
    }

    pathCount += currentGraph.children.reduce(
      (sum, curr) => sum + curr.pathCount,
      0
    );

    currentGraph.pathCount = pathCount;
  }

  return pathCount;
}

let answer = calculateGraphs(0, numbers.length - 1);

endExecution(time, answer, 1157018619904);
