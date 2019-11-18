module Vector =

    type Vector = {
        X: int;
        Y: int;
    }

    let create x y = { X = x; Y = y}
    let add vector1 vector2 = {
        X = vector1.X + vector2.X;
        Y = vector1.Y + vector2.Y;
    }
    let subtract vector1 vector2 = {
        X = vector1.X - vector2.X;
        Y = vector1.Y - vector2.Y;
    }
    let scale vector value = {
        X = vector.X * value;
        Y = vector.Y * value;
    }
    let shrink vector value = {
        X = vector.X / value;
        Y = vector.Y / value;
    }
    
    let (+) = add
    let (-) = subtract
    let (*) = scale
    let (/) = shrink

open Vector

module Rectangle = 

    type Rectangle = {
        TopLeft: Vector;
        BottomRight: Vector;
    }

    let create (topX, topY) (bottomX, bottomY) = {
        TopLeft = Vector.create topX topY;
        BottomRight = Vector.create bottomX bottomY;
    }

module Viewport =

    type Viewport = {
        Size: Vector;
    }

    let create (width, height) = { Size = Vector.create width height }

open Viewport

module Sprite =

    type Sprite = {
        Nickname: string;
        Actual: Vector;
        Relative: Vector;
    }

    let create nickname (x, y) = {
        Nickname = nickname;
        Actual = Vector.create x y;
        Relative = Vector.create x y;
    }

    let setActualPosition sprite actual =
        { sprite with Actual = actual }

    let setRelativePosition sprite relative =
        { sprite with Relative = relative }    

open Sprite

module Player =

    type Player = {
        Nickname: string;
    }

    let create nickname = { Nickname = nickname }

open Player

module Game =

    type Game = {
        Viewport: Viewport;
        Player: Player;
        Sprites: Sprite list;
    }

    let create viewport player sprites = {
        Viewport = viewport;
        Player = player;
        Sprites = sprites;
    }
    
    let private findPlayerSprite game =
        game.Sprites |> List.find (fun sprite -> sprite.Nickname = game.Player.Nickname)

    let private updateSpriteActual sprite actual =
        // update actual for current player
        Sprite.setActualPosition sprite actual

    let private updateSpriteRelative game sprite =
        // find and set relative for other that current player
        let player = findPlayerSprite game
        let offset = player.Actual - sprite.Actual
        // relative for non current player is actually relevant to current
        let relative = 
            if sprite.Nickname = player.Nickname
            then game.Viewport.Size / 2 - offset
            else player.Relative - offset

        Sprite.setRelativePosition sprite relative
    
    let updatePosition nickname (x, y) game =
        let position = Vector.create x y
        // first update the moved player actual position
        let playerFilter (sprite: Sprite) =
            if sprite.Nickname = nickname then sprite
            else updateSpriteActual sprite position
            
        // then update all the other sprite relative positions        
        let spriteFilter = updateSpriteRelative game

        let changedSpritePositions = 
            game.Sprites
            |> List.map playerFilter
            |> List.map spriteFilter

        { game with Sprites = changedSpritePositions }

    
module App = 

    let viewport = Viewport.create (720, 847)
    let sprites = [
        Sprite.create "justmegaara" (1820, 1000);
        Sprite.create "leaveme2010" (1800, 900);
        Sprite.create "sickranchez" (1650, 1100);
        Sprite.create "alienware51" (1800, 1150);
    ]
    let player = Player.create "justmegaara"
    let game = 
        Game.create viewport player sprites
        |> Game.updatePosition "justmegaara" (1820, 1000)
        |> Game.updatePosition "leaveme2010" (1800, 900)
        |> Game.updatePosition "sickranchez" (1650, 1100)
        |> Game.updatePosition "alienware51" (1800, 1150)