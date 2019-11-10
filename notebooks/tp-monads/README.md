# Monoids, Monads and Functors

What is a monad, what is a functor, what is a monoid? Monads: Just, Maybe, Either, Future, Lazy. F# monads: Option, Result, Choice, Lazy.
Define what is lazy evaluation. Show examples of lazy expression in F# and Lazy type in C#.

Resources
https://www.matthewgerstman.com/tech/what-the-functor/
https://www.matthewgerstman.com/tech/mary-had-a-little-lambda/


Let's take a simple concept of function:

```
a c T
f: a -> a
g: a -> a
```


## Monoid

### 1. Composability

```
f(g(a))

or

g(f a)
```

then

```
h = (f o g)
```
where
```
h: a -> a
```

### 2. Identity:

```
id x = x
```

### 3. Associativity

```
f(g a) = g(f a)
```

## Monad


## Resources

- [What the functor?](https://www.matthewgerstman.com/tech/what-the-functor/)
- [Brian Beckman: Don't fear the Monad](https://channel9.msdn.com/Shows/Going+Deep/Brian-Beckman-Dont-fear-the-Monads)