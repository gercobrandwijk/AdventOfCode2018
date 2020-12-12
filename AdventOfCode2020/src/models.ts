export type File = "test" | "test1" | "test2" | "input";

export interface Execution {
  file: File;
  answer: string | number
}
