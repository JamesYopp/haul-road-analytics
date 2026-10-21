# Playwright card: the same loop at the acceptance level

Block 1 content only; later blocks extend this card. Playwright tests drive the application through a browser. They sit at the top of the test pyramid: few, slow, and valuable. They answer "does the whole thing do what the customer needs?" Unit tests underneath answer "is each piece of logic right?" and make the top layer small and stable.

## Setup

```
npm init playwright@latest
npx playwright test
```

## Where the idea of Block 1 applies

An acceptance criterion written as Given, When, Then is already the outline of a Playwright test. The edge cases someone thinks of while writing the AC are the most valuable tests of all, at any level. The loop is the same: write the test, watch it fail, then build until it passes.

```ts
import { test, expect } from '@playwright/test';

test('a truck above 105% of rated capacity is shown as overloaded', async ({ page }) => {
  // Given a truck with rated capacity 220 tonnes carrying 231.01 tonnes
  // When the analyst opens the truck's trip summary
  // Then the load state reads "Overloaded"
  await page.goto('/trips/example');            // adjust to your app
  await expect(page.getByText('Overloaded')).toBeVisible();
});
```

The practice app has no UI, so this card is orientation, not a lab. Apply it to a real feature in an embedded session.
