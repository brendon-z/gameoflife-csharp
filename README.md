# Conway's Game of Life in C#
This is a simple implementation of [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life), which is a zero-player
game demonstrating the evolution of a system from an initial state. I've written this in C# mainly as personal practice.

## How to run
This program is run from the command line. Simply execute the .exe and enter the grid width, height, initial cell counts and tick duration as prompted.

I've used a random Gaussian distribution for the initial seeding of the grid, but it's recommended to use relatively high initial cell counts (>50) to ensure your
system doesn't die out in a few generations.

![{5CE56A2E-936C-46D3-B122-DBA82D4B202B}](https://github.com/user-attachments/assets/51aaedde-b258-442c-81c0-74c2d696d782)
