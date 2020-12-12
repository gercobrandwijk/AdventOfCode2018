export enum PlaceState {
  Floor,
  Available,
  Occupied,
}

export class Place {
  constructor(public state: PlaceState) {}

  aroundOccupied?: number;

  occupiedUpLeft?: boolean;
  occupiedUp?: boolean;
  occupiedUpRight?: boolean;

  occupiedLeft?: boolean;
  occupiedRight?: boolean;

  occupiedDownLeft?: boolean;
  occupiedDown?: boolean;
  occupiedDownRight?: boolean;

  reset() {
    this.aroundOccupied = 0;

    this.occupiedUpLeft = false;
    this.occupiedUp = false;
    this.occupiedUpRight = false;

    this.occupiedLeft = false;
    this.occupiedRight = false;

    this.occupiedDownLeft = false;
    this.occupiedDown = false;
    this.occupiedDownRight = false;
  }

  sumOccupied() {
    let sum = 0;

    if (this.occupiedUpLeft) sum++;
    if (this.occupiedUp) sum++;
    if (this.occupiedUpRight) sum++;
    if (this.occupiedLeft) sum++;
    if (this.occupiedRight) sum++;
    if (this.occupiedDownLeft) sum++;
    if (this.occupiedDown) sum++;
    if (this.occupiedDownRight) sum++;

    return sum;
  }
}

export function fillMatrix(lines: string[]) {
  let matrix: Place[][] = [];

  for (let line of lines) {
    let row: Place[] = [];

    for (let char of line) {
      switch (char) {
        case ".":
          row.push(new Place(PlaceState.Floor));
          break;
        case "L":
          row.push(new Place(PlaceState.Available));
          break;
        case "#":
          row.push(new Place(PlaceState.Occupied));
          break;
      }
    }

    matrix.push(row);
  }

  return matrix;
}

export function printMatrix(matrix: Place[][]) {
  matrix.forEach((row) => {
    let line = "";

    row.forEach((place) => {
      switch (place.state) {
        case PlaceState.Floor:
          line += ".";
          break;
        case PlaceState.Available:
          line += "L";
          break;
        case PlaceState.Occupied:
          line += "#";
          break;
      }
    });

    console.log(line);
  });

  console.log();
}
