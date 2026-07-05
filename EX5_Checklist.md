# EX5 — Grading Checklist (TicTacToeMisere – Windows Forms)

> Based on: EX5 assignment PDF + lecturer Q&A (Facebook group) + Aviel & Ori's graded B25 submission.
> ⚠️ = confirmed point deduction from reference submission or explicit lecturer instruction.

---

## Lecturer Q&A — Key Rulings (from screenshots)

| Question | Lecturer Answer |
|---|---|
| Must we use Visual Designer? | **No** — not required |
| Board must be square — can we use ONE NumericUpDown called "Board Size" instead of separate Rows/Cols? | **Yes** — allowed |
| EX2 board was min=3 max=9 — which limit applies in EX5? | **EX5 rules: min=4, max=10** |
| Replace the "Q" quit from EX2 with what in EX5? | Closing the window (X button) replaces Q — no separate quit button needed |
| Is forfeit needed in EX5? | **No** — not relevant |
| Do we need to change the logic layer (e.g. remove validations no longer needed)? | **No** — no need to change the logic layer |
| Can we use `Button.Tag` to store row/col? | Allowed, but lecturer prefers a custom Button class with its own properties |
| Can we use control properties not explicitly taught (e.g. `label.Font`, `label.TextAlign`)? | **Yes** — you can use any property of controls covered in lectures |
| What player names appear on the game screen? | Whatever the user entered in the settings form |

---

## 1. Functionality

| # | Criterion | ✅ | Notes |
|---|---|---|---|
| 1 | Program runs without errors | [ ] | |
| 2 | FormGameSettings opens on launch | [ ] | |
| 3 | Checking "Player 2" checkbox enables the name TextBox | [ ] | Unchecked → disabled, shows "[Computer]" |
| 4 | Board size NumericUpDown: min=4, max=10 | [ ] | Use EX5 limits, NOT EX2's (3–9) |
| 5 | Board is always square (Rows = Cols) | [ ] | Either one linked NumericUpDown, or two linked ones |
| 6 | Clicking Start closes settings form and opens game form | [ ] | ⚠️ See Critical note below |
| 7 | Game form size adjusts dynamically to board size | [ ] | |
| 8 | Button grid built dynamically according to board size | [ ] | |
| 9 | Clicking a button places the correct symbol (X or O) | [ ] | |
| 10 | Occupied buttons are disabled (`button.Enabled = false`) | [ ] | |
| 11 | Computer makes a move automatically after the human | [ ] | vs-Computer mode only |
| 12 | Win detected → MessageBox shown with winner's name | [ ] | "The winner is [Name]!" |
| 13 | Tie detected → MessageBox shown | [ ] | "Tie!" |
| 14 | MessageBox has Yes / No buttons | [ ] | `MessageBoxButtons.YesNo` |
| 15 | Yes → board resets, score remains, new round starts | [ ] | |
| 16 | No → program exits / game window closes | [ ] | |
| 17 | Score label updates correctly after each round | [ ] | Show user's actual name, not "Player 1"/"Player 2" hardcoded |
| 18 | No forfeit / quit button needed in EX5 | [ ] | Closing the window is sufficient |

---

## 2. Design ⚠️ Most important section

| # | Criterion | ✅ | Notes |
|---|---|---|---|
| 1 | Clean separation between Logic and UI | [ ] | Logic has no reference to `System.Windows.Forms` |
| 2 | No code duplication | [ ] | |
| 3 | Methods are short and focused (flow of calls, not monolithic) | [ ] | |
| 4 | Board uses `eCellSymbol` enum, NOT `char` | [ ] | Explicit lecturer requirement from EX2 grading |
| 5 | Game class creates Player objects (not the UI) | [ ] | Explicit lecturer requirement from EX2 grading |
| 6 | No changes needed to logic layer for EX5 | [ ] | Lecturer confirmed: leave logic as-is |
| 7 | **BONUS: Logic fires `CellChanged` event** | [ ] | Board notifies UI when a cell is set |
| 8 | **BONUS: UI subscribes to the event** | [ ] | `board_CellChanged` updates the button |
| 9 | No hardcoded magic numbers — use `const` / `readonly` | [ ] | ⚠️ -3 from reference (CSS-999) |
| 10 | No lambda expressions or C# features not taught in course | [ ] | ⚠️ -4 from reference (SFN-999) |

### ⚠️ Critical: Form Flow (DSN-999, -2 from reference)
> **Wrong:** `this.Hide()` → `formGame.ShowDialog()` → `this.Show()`
>
> **Correct:** When Start is clicked → **close** the settings form (`this.Close()`).
> In `Program.cs`, after `settingsForm.ShowDialog()` returns, create and `Application.Run()` the game form.
> The settings form must NOT hide itself and re-appear.

---

## 3. Coding Standards

### Naming
| # | Criterion | Code | ✅ |
|---|---|---|---|
| 1 | Local variables: `camelCase` | CSS-001 | [ ] |
| 2 | Fields: `m_PascalCase` | CSS-002 | [ ] |
| 3 | Constants: `k_PascalCase` | CSS-003 | [ ] |
| 4 | Readonly fields: `r_PascalCase` | — | [ ] |
| 5 | Private methods: `camelCase` | CSS-010 | [ ] |
| 6 | Public/protected methods: `PascalCase` | CSS-011 | [ ] |
| 7 | Parameters: `i_` / `o_` / `io_` prefixes | CSS-013 | [ ] |
| 8 | No raw `true`/`false` literals — use named `v_` const booleans | CSS-020 | [ ] |
| 9 | Event handler named `instanceName_EventName` (e.g. `board_CellChanged`) | CSS-021 | [ ] |
| 10 | Event raiser named `OnXXX` | CSS-021 | [ ] |
| 11 | Delegate suffix: `Delegate` or `EventHandler` | CSS-021 | [ ] |

### Structure
| # | Criterion | Code | ✅ |
|---|---|---|---|
| 1 | Always use curly braces — even single-line `if` | CSS-005 | [ ] |
| 2 | Spaces around operators | CSS-025 | [ ] |
| 3 | TAB indentation | CSS-026 | [ ] |
| 4 | Blank line after local variable declarations | CSS-027 | [ ] |
| 5 | Blank line before `return` | CSS-027 | [ ] |
| 6 | Blank line after `}` (unless followed by another `}` or `else`) | CSS-027 | [ ] |
| 7 | Only ONE `return` per method | CSS-028 | [ ] |
| 8 | No hardcoded strings/numbers — use constants | CSS-999 | [ ] | ⚠️ -3 from reference |

---

## 4. .NET / Windows Forms Technologies

| # | Criterion | ✅ | Notes |
|---|---|---|---|
| 1 | `MessageBox.Show(...)` with title + `MessageBoxButtons.YesNo` | [ ] | |
| 2 | `string.Format` for dynamic strings | [ ] | Score label, MessageBox text |
| 3 | `Environment.NewLine` instead of `"\n"` in strings | [ ] | |
| 4 | Button grid created dynamically in code (not Visual Designer) | [ ] | Loop in `initializeGameForm()` |
| 5 | `NumericUpDown` used for board size (min=4, max=10) | [ ] | |
| 6 | `CheckBox` used for Player 2 toggle | [ ] | |
| 7 | `Label` used for score display (not TextBox) | [ ] | |
| 8 | Visual Designer NOT required | [ ] | Confirmed by lecturer |

---

## 5. Submission Rules

| # | Criterion | ✅ |
|---|---|---|
| 1 | Email subject formatted correctly | [ ] |
| 2 | Submission report in email body | [ ] |
| 3 | Report.doc attached | [ ] |
| 4 | ZIP name: `B26 Ex05 ElaShaul 318481066 ShaharNuss 207108847` | [ ] |
| 5 | ZIP contains: solution folder → `.sln` + project folder | [ ] |
| 6 | No `bin`, `obj`, `.git`, `packages` in ZIP | [ ] |
| 7 | Submitted on time | [ ] |

---

## Known Deductions from Reference Submission (Aviel & Ori B25)

| Issue | Points Lost | Code |
|---|---|---|
| Used `this.Hide()` instead of closing the settings form properly | -2 | DSN-999 |
| Hardcoded values instead of constants | -3 | CSS-999 |
| Used lambda expressions (not taught in course) | -4 | SFN-999 |
| **Total lost** | **-9** | |
