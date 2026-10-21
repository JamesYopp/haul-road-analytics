# PRD: Haul Road Analytics

## Problem
Haul trucks in open pit mines carry hundreds of tonnes per trip. Road condition, grade, and curvature change how much fuel a truck burns and how hard it is on the truck. Mine planners want to find road segments that need maintenance or rerouting, using the telemetry trucks already produce: GPS position, speed, fuel consumption, engine readings, and payload.

## Proposed solution
A library, later a service, that ingests truck readings, normalizes them, splits routes into segments, computes fuel burn per segment, and flags segments that look problematic.

## Users
- Mine planner: decides where to maintain or reroute.
- Field operations lead: knows how trucks are actually run and what readings mean.
- Data analyst: builds reports on top of the normalized data.

## Features (delivered one per block)
1. Normalize units and classify load state (Block 1)
2. Fuel burn per segment from a telemetry feed (Block 2)
3. Flag segments above a grade threshold, in existing code (Block 3)
