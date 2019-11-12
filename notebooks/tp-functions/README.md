# Functions

Functional programming languages have being around for more that 60 years. Some of them are the likes of Lisp, Closure, ML, Haskell, F# and many others.

First of all, what is functional programming? This is not a pradigm, it's programming style. Writing programs the functional way does not mean you cannot write some OOP code as well. You can mix the two. Functional programming is simply all about functions.

In functional programmin we have two main building blocks - data and functions.

## PrimitiveTypes

In F# we have the following primitive data types:
- int
- float
- double
- string
- DateTime
- bool
- array
- list
- tuple
- enum

Wheneveer we set a value to a variable we cannot mutate it. In F# they are actually called value bindings. Well, in fact to mutate the value of a binding, wee need to explicitly state that it is mutable. By default all values are immutable. This is how we declare a value binding:


```C#
let value = 42
printfn "The answer is: %i" value

let greeting = "Hello World!"
printfn "%s" greeting

let fsharpIsAwesome = true
printfn "F# is awesome: %A" fsharpIsAwesome
```

    The answer is: 42
    Hello World!
    F# is awesome: true
    




<null>



And to make the value mutable:


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

So what is a function? Function is something that operates on data, it takes an input and produces an output.

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

## Lambda Functions



## Higher-order Functions

Since functions are values, it means that we can pass function into other functions as parameters or return as values. Those type of functions are called `Higher-order Functions`.

Higher-order functions are particulary useful in lots of cases. Assume you have an algorithm that takes and array of values as an input:

## Closures



## Currying

Currying is the technique of translating the evaluation of a function that takes multiple arguments into evaluating a sequence of functions, each with a single argument. [3]

## Partial Application

Partial application (or partial function application) refers to the process of fixing a number of arguments to a function, producing another function of smaller arity. [4]

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


```C#

```
