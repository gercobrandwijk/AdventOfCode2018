"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    Object.defineProperty(o, k2, { enumerable: true, get: function() { return m[k]; } });
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
    __setModuleDefault(result, mod);
    return result;
};
Object.defineProperty(exports, "__esModule", { value: true });
const fs = __importStar(require("fs"));
//let input = fs.readFileSync("src/day02/_test.txt", { encoding: "utf8" });
let input = fs.readFileSync("src/day02/_input.txt", { encoding: "utf8" });
let rows = input.split("\n").map((x) => {
    let parts = x.split(":");
    let ruleParts = parts[0].split(" ");
    return {
        character: ruleParts[1],
        min: parseInt(ruleParts[0].split("-")[0], 10),
        max: parseInt(ruleParts[0].split("-")[1], 10),
        value: parts[1].trim(),
    };
});
let passwordValidCount = 0;
for (let row of rows) {
    let characterAmount = row.value.split("").filter((x) => x === row.character)
        .length;
    if (characterAmount >= row.min && characterAmount <= row.max)
        passwordValidCount++;
}
console.log(passwordValidCount);
