# Functions

Functional programming languages have being around for more that 60 years. Some of them are the likes of Lisp, Closure, ML, Haskell, F# and many others.

First of all, what is functional programming? This is not a pradigm, it's programming style. Writing programs the functional way does not mean you cannot write some OOP code as well. You can mix the two. Functional programming is simply all about functions.

In the world of programming we have two main building blocks - data and functions. Functional languages are not an exception in this case.

## PrimitiveTypes

In F# we have the following primitive data types:


```C#
let integer = 17
printfn "An integer: %i" integer

let fraction1 = 4.07
printfn "A float: %f" fraction1

let fraction2 = double fraction1
printfn "A double: %f" fraction2

let greeting = "World"
printfn "Hello %s!" greeting

let fsharpIsAwesome = true
printfn "F# is awesome: %A" fsharpIsAwesome

let pairOfInts = (3, 5)
printfn "Pair: %A" pairOfInts

let integerArray = [|1;2;3|]
printfn "Array: %A" integerArray

let integerList = [3;4;5]
printfn "List: %A" integerList
```

    An integer: 17
    A float: 4.070000
    A double: 4.070000
    Hello World!
    F# is awesome: true
    Pair: (3, 5)
    Array: [|1; 2; 3|]
    List: [3; 4; 5]
    




<null>



Whenever we set a value to a variable we cannot mutate it. In F# they are actually called value bindings. Well, in fact to mutate the value of a binding, we need to explicitly state that it is mutable. By default all values are immutable.


```C#
let mutable value = 42
printfn "The answer is: %i" value

value <- 17
printfn "The answer changed to: %i" value
```

    The answer is: 42
    The answer changed to: 17
    




<null>



## Functions

Now, we have to process the data. We can do that with a function. So what is a function? Function is something that operates on data, it takes an input and produces an output.

![Function](../../resources/fp-function-definition.png)

First, let's start with a mathematical definition of a function.

```
f(x): int -> int, x c N
```

Here `f` is the name of the function and `x` is the functiona argument. We have defined, that function takes an argument of type `int`, does some transformation of the value and return another instance of type `int`.

This is how you would declare a function in F#:


```C#
// function definition
type Function = int -> int

// function declaration that squares the number
let sqr: Function = fun (x: int) -> x * x
```

We can narrow this down to:


```C#
let sqr x = x * x
```

The left part is value binding - a declaration and initialization of a value. Part after the equal sign is the value itself. In our case it is the function. Functions in F# are treated as values, and so they are first-class citizens.


Some of the built-in functions: printf, printfn, float, int, string.
`printf` workd like `Console.Write` as it takes first parameter which is a strin format and all that follow are the data.
`printfn` works the same as `Console.WriteLine`. We can use `Console.WriteLine` instead of `printfn`, but then we will loose the composibility and the syntax will look a bit odd.

## Lambda Functions

There are times when we need an anonymous function, a one without a name. Those are called lambda functions. This is how a labda is declared:


```C#
let func = fun x ->  x * 2
```

This way a value binding was declared and the value happed to be a function. Just as we did with simple data values. Now this value can be passed around as simple data. This is particulary useful when we want to pass the function as a parameter into other function.

## Higher-order Functions

Functions that take other functions as a parameter or return a function as value are called `Higher-order functions`.

Higher-order functions are particulary useful in lots of cases, like when using `List.map` for example. Let's have a look at the example wherer we want to transform a list of integers. If we were to build a framework, or if we want our functions to be reusable then they need to be flexible.


```C#
let integers = [1;3;6;9]
printfn "Original: %A" integers

let square = fun i -> i * i
let squares = List.map square integers
printfn "Squared: %A" squares
```

    Original: [1; 3; 6; 9]
    Squared: [1; 9; 36; 81]
    




<null>



## Closures

But what if wee need to pass two parameters to the labmda in order to transform the data? How do we do that? For this we have what is called `closure`. A closure is a a feature of the language to catch the reference to a variable in a function where this variable is used. Let me show what I mean on previous example but slightly modified.


```C#
let integers = [1;3;6;9]
printfn "Original: %A" integers

let multiplier = 5
let multiply = fun i -> i * multiplier

let multiplied = List.map multiply integers
printfn "Multiplied: %A" multiplied
```

    Original: [1; 3; 6; 9]
    Multiplied: [5; 15; 30; 45]
    




<null>



In this example the variable `multiplier` is caught in a closure - the scope of the labmda funtion. This way the labda taken only one parameter `i`, but also holds a reference to variable `multiplier`.

## Partial Application

There is another way to make a function that takes less parameters then required. In functional programming this feature is usually called `partial application`. Partial application (or partial function application) refers to the process of fixing a number of arguments to a function, producing another function of smaller arity [4]. Here is a slightly modified previous example.


```C#
let integers = [1;3;6;9]
printfn "Original: %A" integers

let multiply x y = x * y
let multiplier = 5
let multiplyBy5 = multiply multiplier

let multiplied = List.map multiplyBy5 integers
printfn "Multiplied: %A" multiplied
```

    Original: [1; 3; 6; 9]
    Multiplied: [5; 15; 30; 45]
    




<null>



So what is the difference between closure and partial application? If we break thing down to it's core, they are the same. They work the same, at least in F#. The thing is that partial apllication is used on function's last parameter and produces another function which can be used later on. If we compare two versions, then we will see that the one with closure holds a reference and function `mutiply` cannot be changed ever again. It now holds a reference to variable `multiplier` which is bound to value 5. No way to change it. The version with partial application produced a new function with it's own name called `multiplyBy5`, but the original `multiply` has not changed. This is a very neat and flexible way to reuse the functions.

## Currying

This is all possible in F# due to the feature called `curryinng`. Currying is the technique of translating the evaluation of a function that takes multiple arguments into evaluating a sequence of functions, each with a single argument [3]. In fact, in F# every function is already curried. If you look at the function definition, we woul see somethinng like `int -> int -> string`, meaning that the function takes and input parameter of type `int` and return a function that taken another parameter of type `int` and return a type `string`.

## Resources

Primitive types, Unit, tuples, functions (functions without parameters, how functions return result, functions with Unit return result), functions as values, nested functions, anonymous functions, list expression, array expression, “for” expressions, if..then..else expressions.
F# script files, loading files, loading libraries, using Paket, running the scripts, etc.

- [Functional Programming Fundamentals](https://www.matthewgerstman.com/tech/functional-programming-fundamentals/)
- [First Class Functions](https://mostly-adequate.gitbooks.io/mostly-adequate-guide/ch02.html)
- [Overview of types in F#](https://fsharpforfunandprofit.com/posts/overview-of-types-in-fsharp/)
- [Type systems: dynamic versus static, strong versus weak](https://dev.to/jiangh/type-systems-dynamic-versus-static-strong-versus-weak-b6c)
- [Function Values and Simple Values](https://fsharpforfunandprofit.com/posts/function-values-and-simple-values/)
- [Overview of F# expressions](https://fsharpforfunandprofit.com/posts/understanding-fsharp-expressions/)
- [Currying](https://en.wikipedia.org/wiki/Currying)
- [Partial_Application](https://en.wikipedia.org/wiki/Partial_application)
- [Curry and Function Composition](https://medium.com/javascript-scene/curry-and-function-composition-2c208d774983)
- [10 Tips for Productive F# Scripting](https://brandewinder.com/2016/02/06/10-fsharp-scripting-tips/)
