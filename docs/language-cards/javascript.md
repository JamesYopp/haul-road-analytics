# JavaScript / TypeScript card: Vitest

Same loop, different tools. Block 1 content; later blocks extend this card. If a project already uses Jest, the syntax below is nearly identical.

## Setup

```
npm install --save-dev vitest
# package.json: "scripts": { "test": "vitest run" }
npm test
```

Name test files `*.test.ts` or `*.test.js`.

## Anatomy

```ts
import { describe, it, expect } from 'vitest';
import { toTonnes } from './units';

describe('toTonnes', () => {
  it('returns the same value when the unit is tonnes', () => {
    expect(toTonnes(100, 'tonnes')).toBe(100);
  });

  it.each([
    [1, 0.90718474],
    [100, 90.718474],
  ])('converts %d short tons to tonnes', (shortTons, expected) => {
    expect(toTonnes(shortTons, 'shortTons')).toBeCloseTo(expected, 6);
  });

  it('throws when negative', () => {
    expect(() => toTonnes(-1, 'tonnes')).toThrow();
  });
});
```

## The loop

Red: `npm test` fails on an assertion. Green: least code. Refactor with tests green. Commit red before green. Watch mode while you work: `npx vitest`.
