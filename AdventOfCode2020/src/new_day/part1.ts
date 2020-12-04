import * as fs from "fs";
import * as _ from "lodash";
import * as consola from "consola";

let day = "XX";

let input = fs.readFileSync("src/day" + day + "/_test.txt", {
  encoding: "utf8",
});
// let input = fs.readFileSync("src/day" + day + "/_input.txt", {
//   encoding: "utf8",
// });
