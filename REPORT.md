\# Project Report 

\#\# Team Members

- Bryan Russell


\#\# Project Overview  
This project is a third-person survival horror game inspired by titles such as *Resident Evil* and *Silent Hill*. The game will take place in a quarantined modern city after a demonic outbreak disguised as a disease. Players take on the role of a survivor with a natural resistance to demonic possession who must explore hostile environments, manage limited resources, fight infected enemies, and uncover the truth behind a cult-controlled disaster.

\#\# Technical Details

- Game Engine: Unity   
- Programming Language: C\#  
- Target Platform: PC

### \#\# Major Systems Implemented

- Third-person character movement and camera system  
- Over-the-shoulder aiming and shooting  
- Basic enemy AI (idle, chase behavior)  
- Item pickup and interaction system  
- Inventory framework  
- Level grayboxing using ProBuilder  
- Basic combat mechanics 

\#\# Challenges & Fixes

\#\#\# Camera & Movement Issues

- What went wrong:  
  - Initial attempts to recreate a Resident Evil 4–style camera caused diagonal movement issues, where the player would drift away from the camera direction.  
- Fix:  
  - Switched to Unity’s Third Person Controller as a base and modified the movement logic to lock player orientation to the camera unless running.

\#\#\# Level Design Complexity

- Fix:  
  - Used Unity’s ProBuilder to graybox hospital interiors, allowing faster iteration and better gameplay-focused layouts.

\#\#\# Enemy Behavior Bugs

- What went wrong:  
  - Enemies have no animations

\#\# What Worked Well

- Core third-person movement and camera feel

\#\# What You’d Improve

- Add more enemy variety and smarter AI  
- Expand the inventory and crafting systems  
- Improve UI clarity and visual feedback  
- Add additional levels and puzzles  
- Polish animations and sound transitions

\#\# Lessons Learned

- Camera and movement systems are critical and should be prototyped early  
- Grayboxing levels first saves significant development time  
- Iteration and flexibility are key when initial designs don’t work as planned