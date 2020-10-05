## Overview
- This is my submission for Ludum Dare 47 Compo (i.e. solo-developed game in under 48 hours) - the theme was **stuck in a loop**
- tldr; puzzle game about escaping a prison where everything is stuck in a time loop (or is it?)
- Core gameplay concept is deploying bubbles of space free from the prison's time loop. This allows you to save your progress (unlocking doors, etc) even as your character is rewound back in time again and again and again

## Post-mortem

### Choice of genre
After thinking a bit on the theme, I immediately fixated on the core idea of time bubbles and a prison stuck in time. I went ahead with development without considering it further, which caused me several connected problems.

#### Puzzle games aren't my thing
...which I knew before, and yet I still went ahead and tried to make a puzzle game? This created the following problems:
1. **I'm bad at puzzle / level design** - I *loved* (and still do) the core mechanic. But creating levels that challenge the player to use it in interesting ways? Well, that was tricky
2. **I didn't enjoy playtesting, and (also/therefore) wasn't good at it** - I've never been a fan of puzzle games, so I feel that I wasn't  a good playtester, and missed a lot of bugs and issues. Also, I didn't really enjoy it; I found myself just going through the motions, checking that the *intended* path through the game worked, with only minor deviations.

### Inherent annoyingness of the game mechanic
I just want to first say that I still love the idea, and believe it has potential if handled correctly. It wasn't handled correctly in this case, here are several ways the game could've been made less annoying without compromising the core mechanic:

1. **More interesting levels** - due to the time constraint, and me just not being very good/interested in creating sprites, the levels were left very bare. I feel that making the levels more colorful and full of content would've softened the blow of having to walk through them countless times.
2. **Gradual build-up and better explanation** - again something made harder by the game jam format. I feel that the player was thrust into the mechanic with no explanation or consideration. See Note 1 for more regarding this.


### Notes
#### 1. I had to twist and change several instances where the time loop made things confusing/annoying.
Specifically, since everything is on separate loop timers, the player, spending some time in bubbles, inevitably gets out of sync with the world, etc. This created annoying and, at first glance, confusing situations, like starting your loop, rushing to the first floor to move the guard statues, and the guard statue disappearing from your hands because it got reset back in time.

In the end I "resolved" this by updating the timers when the player interacts with certain things to make sure they do what you'd expect them to do.

I think that everything being on separate loops is great, and has potential for some fun and unique level and puzzle design, but it just wasn't to be in this case.

### Takeaways
1. **Create games in genres that you're interested in**
2. **Play to your strengths** - originally I wanted to seed the prison with worldbuilding hints, and end on a tragic note that would change the way you view the run, but these were sacrificed since I had to work on other things. Had I done these things, perhaps the game would've been better off
3. **Get technical stuff squared away first** - I wasted time on downloading and switching the project to Unity 2020 midway through, since I wanted to use the 2D Lighting system. To top this off, including the 2D lights made the WebGL build unplayable (black screen glitch), which I only found out as I was submitting the game, so I was forced to just submit with a Windows executable. Not good. I think submitting with both an executable and an itch.io link with the WebGL version makes the game more accessible to other jammers, so you end up with more feedback and cause less frustration. Takeaway - test out your planned delivery options early and throughout development to avoid the unpleasant surprise (for example, I should've built and tried the WebGL version a lot sooner, or if you're making an Android game, use your phone to constantly build on and test how it feels on a phone, not just in Play Mode in Unity, and if there are any bugs that you don't see in Play Mode
