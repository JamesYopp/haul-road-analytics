# Python card: pytest

Same loop, different tools. Block 1 content; later blocks extend this card. PyCharm and VS Code both run pytest from their test panels.

## Setup

```
python -m venv .venv
source .venv/bin/activate        # Windows: .venv\Scripts\activate
pip install pytest
pytest
```

Put tests in `tests/` with files named `test_*.py`.

## Anatomy

```python
import math
import pytest
from haul_road.units import to_tonnes

def test_to_tonnes_returns_same_value_when_unit_is_tonnes():
    assert to_tonnes(100.0, "tonnes") == 100.0

@pytest.mark.parametrize("short_tons, expected", [(1.0, 0.90718474), (100.0, 90.718474)])
def test_to_tonnes_converts_short_tons(short_tons, expected):
    assert to_tonnes(short_tons, "short_tons") == pytest.approx(expected, abs=1e-6)

def test_to_tonnes_raises_when_negative():
    with pytest.raises(ValueError):
        to_tonnes(-1.0, "tonnes")
```

## The loop

Red: `pytest` fails on an assertion. Green: least code. Refactor with tests green. Commit red before green.

Run one test: `pytest -k to_tonnes`. Stop at first failure: `pytest -x`.
