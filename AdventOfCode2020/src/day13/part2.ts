import * as _ from "lodash";
import { end, readAsLines, start } from "../helpers";
import { Shuttle } from "./shared";

let { time, execution } = start(
  [
    { file: "test", answer: undefined },
    { file: "input", answer: undefined },
  ],
  true
);

let lines = readAsLines("13", execution);

let shuttles = lines[1]
  .split(",")
  .map(
    (x, index) =>
      ({
        value: x === "x" ? -1 : parseInt(x, 10),
        offset: index,
        nextDeparture: 0,
        iterationsForInitial: 0,
        iterationsForCurrent: 0,
      } as Shuttle)
  )
  .filter((x) => x.value !== -1);

let initialShuttle = shuttles[0];

let amount = 0;

for (let i = 1; i < shuttles.length; i++) {
  initialShuttle.nextDeparture = 0;

  let nextShuttle = shuttles[i];

  while (
    nextShuttle.nextDeparture - initialShuttle.nextDeparture !==
    nextShuttle.offset
  ) {
    if (initialShuttle.nextDeparture < nextShuttle.nextDeparture) {
      initialShuttle.nextDeparture += initialShuttle.value;

      nextShuttle.iterationsForInitial++;
    } else {
      nextShuttle.nextDeparture += nextShuttle.value;

      nextShuttle.iterationsForCurrent++;
    }
  }
}

console.log(shuttles);

let answer = undefined;

end(time, answer, execution);
