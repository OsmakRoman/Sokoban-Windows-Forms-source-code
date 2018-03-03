using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_Windows_Forms
{
    public class SokobanCore
    {
        enum MoveDirection
        {
            Up,
            Left,
            Right,
            Down
        }

        public Level Play;
        public event Action EndOfLevelEvent;

        private IList<Level> Levels;
        private Stack<Level> stack;
        private int CurrentLevel;

        public SokobanCore()
        {
            List<Level> list = new List<Level>()
            {
               new Level (22, 11, 13, 9, "    XXXXX                 X   X                 X*  X               XXX  *XXX             X  *  * X           XXX X XXX X     XXXXXXX   X XXX XXXXXXX  ..XX *  *             ..XXXXXX XXXX X@XXXX  ..X    X      XXX  XXXXXX    XXXXXXXX          "),
               new Level (14, 10, 8, 5, "XXXXXXXXXXXX  X..  X     XXXX..  X *  *  XX..  X*XXXX  XX..    @ XX  XX..  X X  * XXXXXXXX XX* * X  X *  * * * X  X    X     X  XXXXXXXXXXXX"),
               new Level (17, 10, 15, 2, "        XXXXXXXX         X     @X         X *X* XX         X *  *X          XX* * X  XXXXXXXXX * X XXXX....  XX *  *  XXX...    *  *   XX....  XXXXXXXXXXXXXXXXXX         "),
               new Level (22, 13, 9, 11, "              XXXXXXXX              X  ....X   XXXXXXXXXXXX  ....X   X    X  * *   ....X   X ***X*  * X  ....X   X  *     * X  ....X   X ** X* * *XXXXXXXXXXXX  * X     X       X   X XXXXXXXXX       X    *  XX            X **X** @X            X   X   XX            XXXXXXXXX             "),
               new Level (17, 13, 15, 8, "        XXXXX            X   XXXXX        X X*XX  X        X     * XXXXXXXXXX XXX   XX....  XX *  *XXXX....    * ** XX X....  XX*  * @X XXXXXXXXX  *  XX         X * *  X         XXX XX X           X    X           XXXXXX "),
               new Level (12, 11, 10, 2, "XXXXXX  XXX X..  X XX@XXX..  XXX   XX..     ** XX..  X X * XX..XXX X * XXXXX * X*  X   X  *X * X   X *  *  X   X  XX   X   XXXXXXXXX"),
               new Level (13, 12, 6, 3, "       XXXXX  XXXXXXX   XXXX X @XX ** XX    *      XX  *  XXX   XXXX XXXXX*XXXX *  XXX ..X X * * * ...X X    XXX...X X ** X X...X X  XXX XXXXX XXXX         "),
               new Level (16, 17, 2, 7, "  XXXX            X  XXXXXXXXXXX  X    *   * * X  X *X * X  *  X  X  * *  X    XXXX *X X  XXXX XX@X* * *  XX   XX    * X*X   X XXX  *    * * * X XXXX  XXXXXXXXX  XXX  XXX        X      X        X      X        X......X        X......X        X......X        XXXXXXXX      "),
               new Level (17, 18, 2, 11, "          XXXXXXX          X  ...X      XXXXX  ...X      X      ...X      X  XX  ...X      XX XX  ...X     XXX XXXXXXXX     X *** XX     XXXXX  * * XXXXXXX   X* *   X   XX@ *  *    *  * XXXXXXX ** * XXXXX     X *    X         XXXX XXX            X  X             X  X             X  X             XXXX     "),
               new Level (21, 20, 3, 6, "              XXXX            XXXXXX  X            X       X            X  XXXX XXX  XXX  XXXXX XXX    X XX@XXXX   *** X    X X **   ** *   X....XXX  ***X    *  X.....XX *   X ** ** X.....XXXX   X  *    X.....X  X   X * * * X.....X  X XXXXXXX XXX.....X  X   X  * *  X.....X  XXX X ** * *XXXXXXX    X X  *      X        X X *** *** X        X X       X X        X XXXXXXXXX X        X           X        XXXXXXXXXXXXX    "),
               new Level (19, 15, 8, 4, "          XXXX          XXXX X  X        XXX  XXX* X       XX   @  *  X      XX  * **XX XX      X  X*XX     X      X X * ** X XXX     X   * X  X * XXXXXXXXX    X  ** X   XXXXX XX *         XX.    XXX  XXXXXXXXX.. ..X XXXX       X...X.X            X.....X            XXXXXXX            "),
               new Level (13, 16, 7, 4, "  XXXXXXXXX    X&.&X&.&X    X.&.&.&.X    X&.&.&.&X    X.&.&.&.X    X&.&.&.&X    XXX   XXX      X   X    XXXXXX XXXXXXX           XX * * * * * XXX * * * * XX X* * * * *X  X   *@*   X  X  XXXXX  X  XXXX   XXXX "),
               new Level (20, 13, 8, 5, "    XXXXXXXXX         XXX   XX  XXXXX   XXX      X  X   XXXXX  ** X* X  X  ... XX X  *X@*XX X X.X. XX  XX X*  X    ... XX *X    * X X X.X. XX    XX  XX* * ... XX * XX   X  X*X.X. XXX **  *   *  *... X X*  XXXXXX    XX  X X   X    XXXXXXXXXX XXXXX              "),
               new Level (17, 13, 8, 5, "XXXXXXXXXXXXXXXX X              X X X XXXXXX     X X X  * * * *X  X X X   *@*   XX XXX X X* * *XXX...XX X   * *  XX...XX XXX*** * XX...XX     X XX XX...XXXXXX   XX XX...X    XXXXX     XXX        X     X          XXXXXXX  "),
               new Level (17, 17, 7, 7, "       XXXX          XXXX  X         XX  X  X         X  * * X       XXX X*   XXXX    X  *  XX*   X    X  X @ * X *X    X  X      * XXXX XX XXXX*XX     X X *X.....X X   X X  *...&. *X XXXXX  X.....X   X  X   XXX XXXXXXX  X **  X  X       X  X     X       XXXXXX   X            XXXXX       "),
               new Level (14, 15, 4, 6, "XXXXX         X   XX        X    X  XXXX  X *  XXXX  X  X  ** *   *X  XXX@ X*    XX  X  XX  * * XX X *  XX XX .X X  X*XX*  X.X XXX   *..XX.X  X    X.&...X  X ** X.....X  X  XXXXXXXXX  X  X          XXXX        "),
               new Level (18, 16, 11, 3, "       XXXXXXX     XXXXXXX     X     X     X *@* X     X** X   XXXXXXXXX X XXX......XX   X X   *......XX X X X XXX......     XXX   XXXX XXX X*XXX  X*   X  *  X X X  * ***  X *XX X X   * * XXX** X X XXXXX     *   X X     XXX XXX   X X       X     X   X       XXXXXXXX  X              XXXX "),
               new Level (22, 13, 16, 3, "      XXXXXXXXXXXX          X  .  XX   X          X X.     @ X     XXXXXX XX...X XXXX   XX  XX...XXXX     XXXXX * XX...    * X  *  XX     .. XX X XX XX  XXXXX*XXX*X *  X   X XX XXX  X    XX* ** X X  X   ** X X * X *XX X  X                  X  XXXXXXXXXXXXXXXXX  X                  XXXX "),
               new Level (28, 20, 13, 2, "        XXXXXX                      X   @XXXX                 XXXXX *   X                 X   XX    XXXX              X *XX  XX    X              X   X  XXXXX X              X X** *    X X              X  * * XXX X X              X X   *  X X X              X X  X*X   X X             XX XXXX   X X X             X  *  XXXXX X X XXXX       XX    *     *  XXX  XXXXXXXXX  XXX * *X * X   .....XX     XX      X  XX  X.....XX ****    XXXXXX*XX   X.XX.XXX    XX              X....X XX  XXXXXXXXXXXXXXX   ....X  X  X             XXXXX  XX  XXXX                 XXXX "),
               new Level (20, 20, 7, 5, "       XXXXXXXXXXXX        X..........X      XXX.X.X.X.X..X      X   .........X      X@ * * * &.&.X     XXXXXXX XXXXXXX  XXXX   X    XX  X  XX    * X    X * XX X  X*X XXX XXX*   XXX *  * *   X * * * XX  X * XX       X* XX   *XXXX*XXXX*XX  XXXXX  XX   X    X  X   X* XX   X X **  X   X   X * X  *    X   XXX X ** X  * XXX     X X    X * XX       X XXXXXXXX X        X          X        XXXXXXXXXXXX   "),
               new Level (16, 14, 3, 11, "   XXXXXXXXXX      X..  X   X      X..      X      X..  X  XXXX   XXXXXXX  X  XX  X            X  X  X  XX  X  XXXXX XX  XXXX XXX  *  XXXXX X  XX X *  *  X *  XX @*  *   X   XXXXXX XX XXXXXXX    X    X          XXXXXX       "),
               new Level (22, 20, 12, 5, "            XXXX       XXXXXXXXXXXX  XXXXX   X    X  X  *  X   XX  X * * *  * X * *   X  XX* *   X @X *   * X XXX   XXXXXXXXXXXX XX X  * *X  X......X *X  X X   X  X......XX X  X  XX XX X .....X  X  X X      *...... * X  X X * XX X......X  X  X  * *X  X......X *X  X *   X  XX*XXXXX  X  X * * XXXX * *  * *X  XX X     * * * *   XXX X  XXXXXX *    *    X X         X XXXXXXX X XXXXXXX X*          X       X   XXXXXXXXXXX       XXXXX          "),
               new Level (25, 14, 6, 8, "       XXXXXXX                  X  X  XXXX               X *X* X  XX       XXXXXXXX  X  X   XXXXXXXXX....  X *X* X  *X  X   XX....X X     X*  X      XX..X.    *X  X *    X*  XX... @XX  X* X*  X  X   XX.... XX *X     *XXXXXXXXXXXXXXXX  X**X*  X              X *X  X  *X              X  X  X   X              XXXX  XXXXX                 XXXX           "),
               new Level (21, 19, 6, 16, "   XXXXXXXXXX           X........XXXX        X.X.X....X  X        X........** X        X     .XXX  XXXX   XXXXXXXXX  * X   X   X     *   * *  * X   X  X    X  * *X  X   XX XXXXX   X  X  X   X *     X   XXXX X  XX  *X   X XX  X  X  X    XX*XXX    X  XX X *    * X  X  X   X XXXXX    X XX X XX XX    X*X X  *  * *   X    X@X  *X***  X   X    XXX  *      XXXXX      XX  X  X  X           XXXXXXXXXX    "),
               new Level (23, 17, 18, 10, "               XXXX              XXXXXX  XXXXX    XXXXXXX       X   X    X      * * XX X X X    X  XXXX *  X     .X    X      * X X XX.X.X    XX*XXXX* * * XX.X.X    X     X    XXXX.XXX    X *   XXXXXX  X.X.XXXXXXX***XX      @X.X.XX      X    X*X*XXX. .XX XXXX X*****    X ...XX X    *     X   X ...XX X   XX XX     XXX...XX XXXXXX*XXXXXX  XXXXXXX        X    X  X     XXXXXXXXXX    XXXX     "),
               new Level (15, 15, 5, 5, "XXXXXXXXX      X       X      X       XXXX   XX XXXX X  X   XX X@XX    X   X *** *  **X   X  X XX *  X   X  X XX  * XXXXXXXX  *** *X  X X   XX   ....X X X   X X.. .X X   X X XX...X XXXXX *  X...X     XX   XXXXX      XXXXX    "),
               new Level (23, 13, 11, 12, " XXXXXXXXXXXXXXXXX      X...   X    X   XXX   XX.....  *XX X X * X   X......X  *  X  *  X   X......X  X  X X X XX  XXXXXXXXX *  * X X  XXX  X     X*XX* XX XX   X XX   *    X *  *   X X X  XX XXX X  XXXXX*X X X * **     *   *     X X *    *XX* XXXXXXXX X XXXXXXX  @ XX      XXX       XXXXXX          "),
               new Level (15, 17, 7, 2, "     XXXXXXX        X@ X  X        X *   X       XXX XX X    XXXX *  X XX   X       X  XX  X * *XXXX * X  X ** X  X  *X  X*  *   X*  X XX  **X   ** XXX **  X  X  * XX     XXXX *  XX  X*XX..XX   XXXX .X....XXXXX  X .......XX    X....   ..X    XXXXXXXXXXX  "),
               new Level (24, 11, 20, 10, "                XXXXX          XXXXXX XXX   XXXX   XXXXX    XXX * *  * XXXXX  XX X* *    * X   XX....   ** * *  *   X*XXX.. X XX X   XXX*XX X  XX....    X XXX    X    XX....    X XX  *  XXX* XX..XXXXXX  *  X  XXXX XXXXXX    X   XXX    @  X         XXXXXXXXXXXXXXX "),
               new Level (14, 20, 9, 7, " XXXXX         X   XXXXXXX   X * XXX   X   X *    ** X   XX XXXX   X  XXX X  X XXX  X   X  X@XX   X **    * X   X   X X * XXXXXXXXX X   X  X X   *XXXX   X X  *     *  X XX   XXXXX XX XXXXXXXXXX  XXX....X *  * XX.....X **X  XX.. ..X *  * XX.....*   X  XXX  XXXXXXXXXX XXXX         "),
               new Level (15, 12, 14, 10, " XXXXXXX        X  X  XXXXX   XX  X  X...XXX X  *X  X...  X X * X** ...  X X  *X  X... .X X   X *XXXXXXXXXX*       * * XXX  X  ** X   X XXXXXX  XX**@X      X      XX      XXXXXXXX "),
               new Level (18, 16, 9, 3, "  XXXX              X  XXXXXXXXX     XX  XX @X   X     X  *X * *   XXXX  X*  *  X * *X  XXXX  *XX X* *     XX  X  X X   ***  XX *    *  *XX XXXXX * * X*X  X  X   XX  XXX  XXX* X    X  X....     X    XXXX......XXXX      X....XXXX         X...XX            X...X             XXXXX          "),
               new Level (13, 15, 2, 5, "      XXXX     XXXXX  X    XX     *X   XX *  XX XXX X@* * X *  X XXXX XX   *X  X....X* * X  X....X   *X  X....  ** XX X... X *   X XXXXXX* *  X      X   XXX      X* XXX       X  X         XXXX   "),
               new Level (12, 15, 11, 11, "XXXXXXXXXXXXXX     XX  XXX   *   * XXXXX XX ** XX   * X    XX *** X XXXXX   X X * XXX  X  X  * XX *X *X    XX   ..X XXXXXXXX.. * X@XX.....X *X XXX....X  * XXXX..XX    XXXXXXXXXXXXX"),
               new Level (20, 16, 11, 2, "XXXXXXXXXXXX  XXXXXXX   X    X@XXXX....XX   **X       .....XX   X XXX   XX ....XXX XX XXX  X   ....X X * *     X XX XXXX X  * *XX  X       XXXXX X  XXXX XX XX XX  X X*   XX XX    XX *  *  X XX XXXXXXXX X * *    X X      X  * XX XX X X      X **     **  X      XX XX XXX *  X       X    X X    X       XXXXXX XXXXXX      "),
               new Level (18, 19, 8, 9, "     XXXX            XXX  XX        XXXX  *  X        X   * *  XXXX     X *   X *   X XXXXX  X  X   * X X..XXX*X* XXXX*XXXX..X X   XXXXX XX ...X X*X XX@XX XX  ..X X X    *     ...X X   XXXX XXX  ..X XXX XX X  XX ...X  XX* XXXX* XXX..X  X   XX    X X..X XX **XX  * X XXXX X     **** X      X * XXX    X      X   X XXXXXX      XXXXX            "),
               new Level (21, 15, 10, 14, "XXXXXXXXXXX          X......   XXXXXXXXX  X......   X  XX   X  X..XXX *    *     X  X... * * X  XXX   X  X...X*XXXXX    X  X  XXX    X   X*  X *XXX  X  ** * *  *XX  * X  X  *   X*X  XX    X  XXX XX X  * XXXXXXX   X  * * XX XX         X    *  *  X         XX   X X   X          XXXXX@XXXXX              XXX          "),
               new Level (14, 15, 11, 4, " XXXXXXXXX     X....   XX    X.X.X  * XX  XX....X X @XX X ....X  X  XXX     X* XX* XXX XXX  *    X X*  * * *X  X X X  * * XX X X  XXX  XX  X X    XX XX XX X  * X  *  X  XXX* *   XXX    X  XXXXX      XXXX       "),
               new Level (23, 18, 12, 6, "              XXX                   XX.XXX                 X....X     XXXXXXXXXXXXX....X    XX   XX     XX....XXXXXX  **XX  * @XX....    XX      ** *X  ....X   XX  * XX ** X X....X  XXX  * XX *  X XX XXX  X XX XXXXX XXX         X XX   *  * XXXXX XXX  X X *XXX  X XXXXX X XXXX X   *   X       X      X  * X* * *XXX  X      X ***X *   X XXXX      X    X  ** X           XXXXXX   XXX                XXXXX             "),
               new Level (11, 11, 9, 2, "      XXXX XXXXXXX @X X     *  X X   *XX *X XX*X...X X  X *...  X  X X. .X XX X   X X* X X*  *    X X  XXXXXXX XXXX      "),
               new Level (20, 15, 18, 9, "           XXXXX              XX   XX            XX     X           XX  **  X          XX **  * X          X *    * X   XXXX   X   ** XXXXX X  XXXXXXXX XX    X X..           ***@X X.X XXXXXXX XX   XX X.X XXXXXXX. X* *XXXX........... X   * XXXXXXXXXXXXXXX  *  X             XX  XXX              XXXX  "),
               new Level (13, 18, 3, 2, " XXXXXXXX     X@XX   XXXX  X *   *   X  X  * * ***X  X **X X   X XX*    *   X X  *  *****XXX *XXXX X   XX  *....X   XX XX....X** XX XX....   XXX   ....X  X XX X....X**X  X X....X  X  X         X  XXXX XX*XXX     X    X       XXXXXX   "),
               new Level (17, 16, 16, 4, "    XXXXXXXXXXXX     X          XX    X  X X** *  X    X* X*X  XX @X   XX XX X * X XX   X   * X*  X X    X   X *   X X    XX * *   XX X    X  X  XX  * X    X    XX **X X XXXXXX**   X   X X....X  XXXXXXXX X.X... XX        X....   X        X....   X        XXXXXXXXX        "),
               new Level (25, 19, 14, 8, "      XXXXXX                XXXXX   X                X   X X XXXXX            X * X  *    XXXXXX      XX*  XXX XX       X    XXX  ** * * X  XX   XXXXXX       *   XXXXXX XX   XX  XXXXXXXX X@   X X  X XXX XXX      XXXX X*X X  X X XXX XXXX XX.. X   * XX X  *  *  X*XX.. X*XX  XX X  X X X     ..XX XX * X XXXX   X XX X..X    *  X    XXXXX    X..X X X  XX        XXXXXX..X   X XX              X..XXXXX  X              X..       X              XX  XXX  XX               XXXXXXXXX  "),
               new Level (19, 11, 10, 8, "        XXXXXXX        XXXXX  X  XXXX     X   X   *    X  XXXX X** XX XX  X XX      X X  XX XXXX  XXX *X*  *  *  XX...    X XX  X   XX...X    @ X XXX XXX...X  XXX  *  *  XXXXXXXXX XX   X   X          XXXXXXXXX"),
               new Level (22, 17, 12, 15, "    XXXXXXXXX  XXXX       X   XX  XXXX  X       X   *   X  *  X       X  X XX X     XXXX    XX *   * **X X   X    XXXX  X  X * *   XXXXXX  XXXX    XXX...XX   X* X  X XXXX.....XX      X  X X XX.....XXXXXXX X  X*   XXX...X   X   XX X *X   X...X  XX       *  *X XXXXX XX ***XX  X *   X     X   X  X XXX  XXX     X   *  X* @XXXX       XXXXX  X   X              XXXXXXXX         "),
               new Level (19, 15, 10, 4, " XXXXX              X   X              X X XXXXXX         X      *@XXXXXX    X * XX* XXX   X    X XXXX *    * X    X XXXXX X  X* XXXXXX  XXXX XX*      XX  *X  *  X XX XX XX         X X...X XXXXXXX  XXX  ...  X     XXXX X X...X X          X XXX X X          X       X          XXXXXXXXX"),
               new Level (16, 15, 8, 2, "       XXXX            X  XX           X   XX          X ** XX       XXX*  * XX   XXXX    *   X XXX  X XXXXX  X X    X X....* X X X   * ....X X X  * X X.&..X X XXX  XXXX XXX X   XXXX @*  XX*XX     XXX *     X       X  XX   X       XXXXXXXXX"),
               new Level (19, 16, 3, 8, "      XXXXXXXXXXXX      XX..    X   X     XX..& *    * X    XX..&.X X X* XX    X..&.X X X *  X XXXX...X  X    X X X  XX X          X X @* * XXX  X X XX X *   *   X X   X  XXX**   X X X X X    X   *   X X XXXXX  X *X XXXXX      X  X*   X   X   X  X  X  XXX   XX     X  X  X      X    XX  XXXX      XXXXXX "),
               new Level (21, 16, 6, 10, "     XXXXXXXXXXXXX        X    XXX    X        X     * *  XXXX    XXXX X   * *    X   XX *  X*XXXX * * X XXX   X X   XXX  * X X *  *  X  *  X XXXX X XX*XXXX X*X  *  XXXX XX  XXX X X X  *  XX    @*   *   X * X XXXXXX  X  XX  X *X  X  X... XXXXX*  X  X X  X.......X ** X* X X  X.......X         X  X.......XXXXXXX  XX  XXXXXXXXX     XXXX "),
               new Level (16, 14, 6, 10, "XXXXX XXXX      X...X X  XXXX   X...XXX  *  X   X....XX *  *XXX XX....XX   *  X XXX... XX * * X X XX    X  *  X X  XX X XXX XXXXX * X X*  *    XX  * @ *    *  XX   X * ** * XXXX  XXXXXX  XXX  X XX    XXXX    XXX             "),
               new Level (21, 14, 6, 13, " XXXX                XX  XXXXX            X       X XXXXX      X *XXX  XXX   X      X..X  *X X  X X      X..X      **X XXX    X.&X X  X* *    XXXXXX..X  XX     XX*X   XX.&*  * X XX  *     XX..XX  *   X   XXXXXXX.&XX*XX   XXXXX     X..  * XXXXX         X  X @ X             XXXXXXXX             "),
               new Level (13, 19, 5, 8, "   XXXXXXXXXX   X  XXX   X   X *   *  X   X  XXXX*XX   XX X  X  X  XX  X.&   X  X  XX..X  X  X @ X.&X XX  X X*X..X* X  X * X..X  X  X X X&&X  X  X * X..X*XX  X    .&X  X XXX  X  X  XXX    XXXX  XX  XXXXXXX*XXX *      *  XX  XX   X   XXXXXXXXXXXXXX"),
               new Level (23, 20, 5, 12, " XXXXXXXXXXXXXXXXXXXXX  X   XX  X   X   X   X  X *     *   *   *   XXXXXXX X  X   XXX XX*XXXX   X XX*XXXXXX   X   XX *   X ......X   X * XXX X  X ......XXXXX   XXX XXXXXXXXX..X   X XXXX          X..X *   X  X XX XXX XXX..XX X  XXXX X   X   XX..XX XXX  XX   @      *..X       XX X   X   XX  X   XX  XXXXXX XXXXXXXXXXXXXX XXX          X   X    * XX *  X * * *   X X    XX X*XX *X  XX XX    X XX  * ** XXXX *  * X X XX          X   X      XXXXXXXXXXXXXXXXXXXXXXXX"),
               new Level (22, 15, 6, 9, " XXXXXXXXXXXXXXXXXXXXXXX                   XX    * X      XX X   XX  XXXXXX XXX  X*XX XXXX*X   XX*X....   X X X  X    * X....XX X X X * X X X X....XX   X X * X**   X....XX*X X X X *@*XX*X....XX   X X   ***   X....X    X X  *X   X XXXXXX *XXX XX  X XXX**  *   * X  XX     X *  * XX   X   XXXXX   X   XXXXXXX       XXXXXXXXX        "),
               new Level (14, 16, 12, 8, "XXXXXXXXXX    X        XXXX X XXXXXX X  XXX X * * *  * XX       X*   XXXX*  **X  XXX  X  XX X *XX   XX*X   * @X    X  * * XXX    X X   *  X    X XX   X X   XX  XXXXX X   X         X   X.......XXX   X.......X     XXXXXXXXX   "),
               new Level (18, 11, 8, 6, "         XXXX      XXXXXXXXX  XX    XX  *      * XXXXXX   XX XX   XX...XX X** * **X*XX...XX X    @  X   ...XX  *X XXX**   ...XX *  **  * XX....XXXX*       XXXXXXX  X  XXXXXXX        XXXX            "),
               new Level (27, 20, 22, 15, "              XXXXXX                 XXXXX    X                 X  XX X  XXXXX             X   &.X..X   X    XXXXX XXXX *X.X...    X    X   XXX  XX X&....XX XX    X *      XX X..X..XX X    XXXXXX X   X X&.XXXXX X    X   X *X*X X X..XXXXX X    X *  *     X X&.    X X    XX XX  * XXX X  XX  X X     X  *  * XXX XXXXX XX X     XXX*XXX*XXX  XXXX XX X    XXXX X         XXX  X X    X  * X  *XXXX  XXX**X@XXXXXX      * X X  XXXX  X*X   XXXXX X  *X X              X   X  *  X XX  XX  XXXXXXXX   XX  XXX  XXXXXXXX           XXXX                   "),
               new Level (29, 20, 14, 14, "         XXXX                         X  X                         X  XXXXXXXX            XXXXXXX  X      X            X   X X X X X   XX           X *     *  XX  * X          XXX *X X  X X     XXXXXXXXX  X  *  X  *X X ** X   X X  X XX X   X     XXX    * X X  X X  X*   X XXX  X  X **X X  X X    *XX *  X   XX *  X X XXXXXX* * X    XX  X   *    ..XX  X    XXX X * * XXX  XXX.&XX     XX  ** @  *     XX....XX  XX  XX   *  X*X  XX....&.XXX X  *  X X *XX  XX....&.XXXXX XX  *  X * X  X....&.XXX  X    * XXXX   X ....&.XXX    X   X  X  X  X  ..&.XXX      XXXXXXXX  XXXXXXXXXXX        "),
               new Level (26, 16, 7, 9, "        XXXXX                     X   XXXX                  X *    XXXX  XXXX         X   X *X  XXXX  X XXXXXXXXXXX X   *   X   X X..     X *  XXXX X  X  X X..*  X   *  X  * X * .XX X.&X X * * XX  XX    X.X  X..X* @ X   XX    ** X.X  X..X * *  * * XX   XX .X  X.&** X XX   * X*X * X.X  X..X      XX   X     X.X  X..XXXXXXX  XXX XXXXXX.XX X **                  &.XXX  XXXXXXXXXXXXXXXXXX  ..XXXXX                XXXXXX"),
               new Level (22, 11, 13, 9, "    XXXXX                 X   X                 X   X               XXX   XXX             X       X           XXX X XXX X     XXXXXXX   X XXX XXXXXXX    XX             *     .XXXXXX XXXX X@XXXX    X    X      XXX  XXXXXX    XXXXXXXX          ")

            };

            Levels = list.AsReadOnly();

            stack = new Stack<Level>();

            CurrentLevel = 1;
            SetLevel (CurrentLevel);
        }

        public int GetCurrentLevel()
        {
            return CurrentLevel;
        }
        public void SetLevel (int number)
        {
            if (number == 61) number = 1;
            Play = new Level(Levels[number - 1]);
            CurrentLevel = number;
            stack.Clear();
        }

        public bool ResetLevel()
        {
            SetLevel (CurrentLevel);
            return true;
        }

        public bool MoveUndo()
        {
            if (stack.Count == 0)
                return false;
            Play = new Level(stack.Pop());
            return true;
        }

        void EndLevelTest()
        {
            for (int i = 0; i <= Play.SizeY - 1; i++)
                for (int j = 0; j <= Play.SizeX - 1 ; j++)            
                    if (Play.Blocks[i * Play.SizeX + j] == '*')
                        return;

            EndOfLevelEvent();
        }

        public bool TryMoveLeft()
        {
            if (DoMoveLogic(MoveDirection.Left))
                return true;
            return false;
        }

        public bool TryMoveRight()
        {
            if (DoMoveLogic(MoveDirection.Right))
                return true;
            return false;
        }
        public bool TryMoveUp()
        {
            if (DoMoveLogic(MoveDirection.Up))
                return true;
            return false;
        }
        public bool TryMoveDown()
        {
            if (DoMoveLogic(MoveDirection.Down))
                return true;
            return false;
        }

        private bool DoMoveLogic (MoveDirection moveDirection)
        {
            int position  = 0;
            int nextposition = 0;
            int nextnextposition = 0;

            switch (moveDirection)
            {
                case MoveDirection.Left:
                    position = ((Play.PusherY - 1) * Play.SizeX + Play.PusherX) - 1;
                    nextposition = ((Play.PusherY - 1) * Play.SizeX + Play.PusherX) - 2;
                    nextnextposition = ((Play.PusherY - 1) * Play.SizeX + Play.PusherX) - 3;
                    break;
                case MoveDirection.Right:
                    position = ((Play.PusherY - 1) * Play.SizeX + Play.PusherX) - 1;
                    nextposition = ((Play.PusherY - 1) * Play.SizeX + Play.PusherX);
                    nextnextposition = ((Play.PusherY - 1) * Play.SizeX + Play.PusherX) + 1;
                    break;
                case MoveDirection.Up:
                    position = ((Play.PusherY - 1) * Play.SizeX + Play.PusherX) - 1;
                    nextposition = ((Play.PusherY - 2) * Play.SizeX + Play.PusherX) - 1;
                    nextnextposition = ((Play.PusherY - 3) * Play.SizeX + Play.PusherX) - 1;
                    break;
                case MoveDirection.Down:
                    position = ((Play.PusherY - 1) * Play.SizeX + Play.PusherX) - 1;
                    nextposition = ((Play.PusherY) * Play.SizeX + Play.PusherX) - 1;
                    nextnextposition = ((Play.PusherY + 1) * Play.SizeX + Play.PusherX) - 1;
                    break;

            }

            if ((Play.Blocks[nextposition] == 'X' ||
                (Play.Blocks[nextposition] == '*' && (Play.Blocks[nextnextposition] == 'X' || Play.Blocks[nextnextposition] == '*' || Play.Blocks[nextnextposition] == '&'))||
                (Play.Blocks[nextposition] == '&' && (Play.Blocks[nextnextposition] == 'X' || Play.Blocks[nextnextposition] == '*' || Play.Blocks[nextnextposition] == '&'))))
                return false;

            stack.Push(new Level(Play));

            if (Play.Blocks[nextposition] == '*'|| Play.Blocks[nextposition] == '&')
            // Перерисовываем контейнер
            {
                if (Play.Blocks[nextnextposition] == ' ')
                    Play.Blocks = Play.Blocks.Remove(nextnextposition, 1).Insert(nextnextposition, "*");
                else if(Play.Blocks[nextnextposition] == '.')
                    Play.Blocks = Play.Blocks.Remove(nextnextposition, 1).Insert(nextnextposition, "&");
            }

            // Перерисовываем игрока
            Play.Blocks = Play.Blocks.Remove(nextposition, 1).Insert(nextposition, "@");

            // Перерисовываем за игроком
            if (Levels[CurrentLevel - 1].Blocks[position] == '.' || Levels[CurrentLevel - 1].Blocks[position] == '&')
                Play.Blocks = Play.Blocks.Remove(position, 1).Insert(position, ".");
            else
                Play.Blocks = Play.Blocks.Remove(position, 1).Insert(position, " ");

            switch (moveDirection)
            {
                case MoveDirection.Left:
                    Play.PusherX = Play.PusherX - 1;
                    break;
                case MoveDirection.Right:
                    Play.PusherX = Play.PusherX + 1;
                    break;
                case MoveDirection.Up:
                    Play.PusherY = Play.PusherY - 1;
                    break;
                case MoveDirection.Down:
                    Play.PusherY = Play.PusherY + 1;
                    break;
            }

            EndLevelTest();
            return true;
        }
    }
}
