# Feature PRD: Normalize and classify truck readings

Input to `create-epics-stories` for Block 1. Treat the Field Operations lead as your customer proxy; the open questions at the end are for them.

## Problem
Readings arrive from different truck models and sites in mixed units. Some trucks report metric, others US customary. Downstream analysis (fuel burn per segment, road flagging) is wrong if units are mixed. Analysts also need to know whether a truck was hauling a load or running empty, because fuel burn and road stress differ a great deal between the two.

## Goals
- Every reading the analytics code sees is in one unit system.
- Every reading carries a load state.
- Bad data does not quietly produce wrong numbers.

## In scope
- Convert payload to tonnes, speed to kilometres per hour, and fuel to litres.
- Classify load state from payload and the truck's rated capacity.

## Out of scope
Ingesting readings, route segmentation, fuel burn calculation, any UI.

## Business rules (from Field Operations)
1. Output units are metric: payload in tonnes, speed in km/h, fuel in litres.
2. Readings already in metric units are returned unchanged.
3. Conversion factors:
   - 1 short ton = 0.90718474 tonnes
   - 1 mph = 1.609344 km/h
   - 1 US gallon = 3.785411784 litres
4. Load state is based on payload as a share of rated capacity:
   - Empty: below 10% of rated capacity
   - Loaded: from 10% up to and including 105%
   - Overloaded: above 105%
5. Bad readings must be handled sensibly.

## Success
Analysts can trust that all values are metric and that every reading has a load state, without checking.

## Open questions
Resolve these with your customer proxy before you plan. Do not guess.
- What counts as a bad reading?
- What should happen to a bad reading?
- Which unit-specific rules differ between payload, speed, and fuel?
