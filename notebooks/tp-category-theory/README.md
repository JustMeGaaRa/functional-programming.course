# Functors, Monoids, Monads

The world of programming is a complex world nowerdays. There is so many libraries, so many frameworks, so many thinngs to program against. This requires a way to deal with all this complexity that's piled up in each and every ptojecct over the years. Now, we need to support those project for couplle of years, even tens of years afterwards, right? And we want not to just deal with legacy code, but to actually have it up to date, have it clean and easy to understand.

Right now, what OOP offers has failed with it's task. For further reading I suggest a blog post on Medium called ["Object-Oriented Programming — The Trillion Dollar Disaster"](https://medium.com/better-programming/object-oriented-programming-the-trillion-dollar-disaster-92a4b666c7c7) by Ilya Suzdalnitski.

In functional programming we deal with complexity by combining little pieces toghether. Those pieces are actually functions. And we want them to be simple, decoupled and easy to undnerstand, so that we have a set of building blocks which can be used and combined in different ways. Like when we're building a house with bricks - small building blocks that help us to create all those different and unique houses that suit our needs.

## Functor

Now, how do we combine functions? Let's look at the example:


```C#
type Fruit =
    | Apple
    | Bananna
    | Cherry
    
let 
```


    input.fsx (6,1)-(6,5) parse error Incomplete structured construct at or before this point in binding


## Resources

- [Object-Oriented Programming — The Trillion Dollar Disaster](https://medium.com/better-programming/object-oriented-programming-the-trillion-dollar-disaster-92a4b666c7c7)
- [A Fistful of Monads](http://learnyouahaskell.com/a-fistful-of-monads)
- [Functors, Applicative Functors and Monoids](http://learnyouahaskell.com/functors-applicative-functors-and-monoids)
- [For a Few Monads More](http://learnyouahaskell.com/for-a-few-monads-more)
- [Brian Beckman: Don't fear the Monad](https://channel9.msdn.com/Shows/Going+Deep/Brian-Beckman-Dont-fear-the-Monads)
- [What the Functor?](https://www.matthewgerstman.com/tech/what-the-functor/)
- [Your easy guide to Monads, Applicatives, & Functors](https://medium.com/@lettier/your-easy-guide-to-monads-applicatives-functors-862048d61610)
- [Explaining monads](http://www.ouarzy.com/2017/09/27/explaining-monads/)
- [Category Theory for Programmers: The Preface](https://bartoszmilewski.com/2014/10/28/category-theory-for-programmers-the-preface/)
- [IO monad: which, why and how](https://kubuszok.com/2019/io-monad-which-why-and-how/)
- [From Haskell to F#](https://giuliohome.wordpress.com/2019/04/08/from-haskell-to-f/)
- [From design patterns to category theory](https://blog.ploeh.dk/2017/10/04/from-design-patterns-to-category-theory/)
- [Monads explained in C#](https://mikhail.io/2016/01/monads-explained-in-csharp/)
- [Monads explained in C# (again)](https://mikhail.io/2018/07/monads-explained-in-csharp-again/)
- [Introduction to Functors in C#](https://medium.com/@dimpapadim3/monads-in-oop-with-c-a4ec11f1f9d9)
- [Monoids in C#](https://medium.com/@dimpapadim3/monoids-in-oop-with-c-42060d3495a7)
- [Monads for object oriented programming with C#](https://medium.com/@dimpapadim3/monads-in-oop-with-c-a4ec11f1f9d9)
- [Functor, Applicative, and Why](https://medium.com/axiomzenteam/functor-applicative-and-why-8a08f1048d3d)
- [Option, Either, State, and IO: Imperative programming in a functional world](https://medium.com/disney-streaming/option-either-state-and-io-imperative-programming-in-a-functional-world-8e176049af81)


```C#

```
