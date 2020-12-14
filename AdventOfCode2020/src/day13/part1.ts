import * as _ from "lodash";
import { end, readAsLines, start } from "../helpers";
import { Shuttle } from "./shared";

let { time, execution } = start(
  [
    { file: "test", answer: 295 },
    { file: "input", answer: 410 },
  ],
  false
);

let lines = readAsLines("13", execution);

let firstPossibleDeparture = parseInt(lines[0]);

let shuttles = lines[1]
  .split(",")
  .filter((x) => x !== "x")
  .map(
    (x) =>
      ({
        value: parseInt(x, 10),
      } as Shuttle)
  );

let upcomingShuttle: Shuttle = null;

for (let shuttle of shuttles) {
  shuttle.nextDeparture =
    firstPossibleDeparture - (firstPossibleDeparture % shuttle.value);

  shuttle.nextDeparture += shuttle.value;

  if (
    !upcomingShuttle ||
    shuttle.nextDeparture < upcomingShuttle.nextDeparture
  ) {
    upcomingShuttle = shuttle;
  }
}

let answer =
  (upcomingShuttle.nextDeparture - firstPossibleDeparture) *
  upcomingShuttle.value;

end(time, answer, execution);
