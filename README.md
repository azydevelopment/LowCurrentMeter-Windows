# template-readme
TLDR: Arduino component for displaying the output of an AD8428 amplifier + ATmega32u4 used as a current meter.

## The context
Short version is that I needed to measure very low currents to measure the power consumption of my battery powered projects.

## The problem
Bottom line, I couldn't find any (non-exorbitantly priced) tools for this that had the bandwidth required for the high frequency changes in current draw typical of microelectronics.

At first I'd thought I'd use this https://www.eevblog.com/projects/ucurrent/ but the reported bandwidth is only 300KHz, much slower than what I'd expect I'd need for such fast current changes.

Then I came across this https://www.dorkbotpdx.org/blog/paul/measuring_microamps_milliamps_at_3_mhz_bandwidth and I decided to build my own; only better.

## The solution

* Micro-USB (female) input from my PC
* AD8428 (super awesome) amplifier
* MEV1D0515DC dual rail 15V supply for the AD8428
* Precision resistors as current shunts (eg. 1 Ohm, even down to 0.01 Ohms for a wider current measurement range at the cost of accuracy)
* USB-A (female) connector to passthrough data lines from the micro USB input
* Arduino ATmega32u4 that outputs the output of the AD8428 to serial (ideally in batches)
* Windows UI component that displays the data coming over serial from the Atmega32u4

## The details

TBD