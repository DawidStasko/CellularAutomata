<p style="text-align: center;">Desktop application to explore a probelm of cellular automata. This implementation is a proprietary solution based on existing mathematical models.</p>
<p>For recruitment purposes: This project is being actively developed. Code evolution, from smallest working application to one using design patterns and unit tests can be observed. The project is also used for demonstration and trainning, so it may contain some overkill solutions for such small project.</p>
<p>Introduction to cellular automata from wikipedia:<br />A cellular automaton is a discrete model of computation studied in automata theory (study of abstract machines and automata, as well as the computational problems that can be solved using them). It consists of a regular grid of cells, each in one of a finite number of states, such as on and off.For each cell, a set of cells called its neighborhood is defined relative to the specified cell. An initial state (time t = 0) is selected by assigning a state for each cell. A new generation is created (advancing t by 1), according to some fixed rule (generally, a mathematical function) that determines the new state of each cell in terms of the current state of the cell and the states of the cells in its neighborhood. Typically, the rule for updating the state of cells is the same for each cell and does not change over time, and is applied to the whole grid simultaneously</p>
<p>App features:</p>
<ul>
<li>Conway game of life - most widely known example of cellular automata, devised by the British mathematician John Horton Conway in 1970. The universe of the Game of Life is an infinite, two-dimensional orthogonal grid of square cells, each of which is in one of two possible states, live or dead (or populated and unpopulated, respectively). Every cell interacts with its eight neighbours, which are the cells that are horizontally, vertically, or diagonally adjacent. At each step in time, the following transitions occur:
<ol type="1">
<li>Any live cell with fewer than two live neighbours dies, as if by underpopulation.</li>
<li>Any live cell with two or three live neighbours lives on to the next generation.</li>
<li>Any live cell with more than three live neighbours dies, as if by overpopulation.</li>
<li>Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.</li>
</ol>
These rules, which compare the behavior of the automaton to real life, can be condensed into the following:
<ol type="1">
<li>Any live cell with two or three live neighbours survives.</li>
<li>Any dead cell with three live neighbours becomes a live cell.</li>
<li>All other live cells die in the next generation. Similarly, all other dead cells stay dead.</li>
</ol>
What user can actually do in application:
<ul>
<li>Draw rectangle board.</li>
<li>Change state of cell by clicking on it.</li>
<li>Calculate next generation based on Moore's neighborhood.</li>
</ul>
</li>
</ul>
