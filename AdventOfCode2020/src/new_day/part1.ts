import * as _ from "lodash";
import { end, readAsLines, start } from "../helpers";

let { time, execution } = start(
  [
    { file: "test", answer: undefined },
    { file: "input", answer: undefined },
  ],
  true
);

let lines = readAsLines("XX", execution);

//////////
//////////
//////////
//////////
//////////
//////////

let answer = undefined;

end(time, answer, execution);
