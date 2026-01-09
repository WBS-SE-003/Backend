function sayHello(name) {
  console.log(`Hello, ${name}`);
}

sayHello('Alice');

// Value vs Reference Types
// default behaivour
let x = 5;
increment(x);

console.log(x); // still 5

function increment(number) {
  number++;
  console.log(number); // 6
}

// pass by ref

let obj = { value: 5 };
increment(obj);

console.log(obj.value); // 6

function increment(o) {
  o.value++;
}

function tryDivide(a, b) {
  if (b === 0) {
    return { success: false, result: 0 };
  }
  return { success: true, result: a / b };
}

const { success, result } = tryDivide(10, 2);

console.log(tryDivide(10, 0));
