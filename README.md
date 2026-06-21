# Amibe

**Noah Loute | 2025/2026**

Amibe is an interactive installation exploring human curiosity and its impact on a self-sustaining ecosystem. The project simulates a microscopic world made of autonomous cells, prey that eat, divide and occasionally mutate into predators, maintaining a natural prey-predator balance inspired by the Lotka-Volterra model.

The ecosystem lives and evolves on its own. It does not need human intervention. Yet, through a physical interface, the visitor can introduce their own cell into the system, an omnivore that disrupts the balance that had settled. The project asks a simple question: why do we interfere with something that was working fine without us?

![A video of the ecosystem](resource/example.gif)

---

## How it works

The simulation runs in real time using Unity 6 (URP 2D). Each cell is an autonomous agent with its own energy, behavior and lifespan. Prey seek food, divide when full and can mutate into predators upon division. Predators hunt prey. If the food runs out, everything collapses.

The visitor interacts through a physical box equipped with a copper plate. By holding their hand on it, they charge the sending of an omnivore cell into the ecosystem. There is no choice to make, the act itself is the perturbation.

The interface communicates with the simulation via a serial connection (Microbit or Arduino). The expected serial format is a single 0 or 1 sent on one line.

Example of expected serial input:

```
1
```

This single value corresponds to the hand sensor (spawn trigger). A value of 1 means the hand is currently on the plate, 0 means it is not.

The Microbit programs used for this project can be found in the `microbit` folder.

---

## Background

This project grew out of an exploration of _The Nature of Code_ by Daniel Shiffman, a book about simulating natural phenomena through creative coding. Starting from basic concepts like vectors and forces, the project evolved toward autonomous agents, steering behaviors and emergent ecosystems.

Along the way, ideas like Conway's Game of Life and Craig Reynolds' Boids flocking system were explored as references for how simple rules can produce complex, lifelike behavior. Amibe is the result of applying these principles to a prey-predator dynamic, with human interaction as the central artistic question.

---

## Prototype of the instalation

![Photo of the box](resource/box.png)
![Photo of the entire installation](resource/reflection.png)

---

## Tech stack

- Unity 6 (URP 2D)
- C#
- Microbit / Arduino (serial communication)
- Reaper (sound design)

---

## License

This project is licensed under CC BY-NC 4.0 - see LICENSE for details.
