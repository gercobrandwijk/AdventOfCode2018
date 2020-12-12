import * as _ from "lodash";
import { end, readAsLines, start } from "../helpers";

let { time, execution } = start([
  { file: "test1", answer: 8 },
  { file: "test2", answer: 19208 },
  { file: "input", answer: 1157018619904 },
]);

let lines = readAsLines("10", execution);

let numbers = lines.map((x) => parseInt(x, 10));
numbers.sort((x, y) => x - y);

numbers.unshift(0);
numbers.push(numbers[numbers.length - 1] + 3);

class Graph {
  index: number;
  count: number;
  children: number[];
}

let graphs: { [index: number]: Graph } = {};

for (let i = 0; i < numbers.length; i++) {
  graphs[i] = {
    index: i,
    count: null,
    children: [],
  };

  let j = i + 1;

  while (numbers[j] <= numbers[i] + 3) {
    graphs[i].children.push(j);

    j++;
  }
}

function calculateGraphs(currentIndex: number, endIndex: number): number {
  let pathCount = 0;

  let currentGraph = graphs[currentIndex];

  if (currentIndex === endIndex) {
    pathCount++;

    currentGraph.count = pathCount;
  } else {
    let childIndexesToCount = currentGraph.children
      .map((childIndex) => ({ index: childIndex, child: graphs[childIndex] }))
      .filter((item) => item.child.count === null)
      .map((item) => item.index);

    for (let childIndex of childIndexesToCount) {
      calculateGraphs(childIndex, endIndex);
    }

    pathCount += currentGraph.children.reduce(
      (sum, childIndex) => sum + graphs[childIndex].count,
      0
    );

    currentGraph.count = pathCount;
  }

  return pathCount;
}

let answer = calculateGraphs(0, numbers.length - 1);

end(time, answer, execution);
