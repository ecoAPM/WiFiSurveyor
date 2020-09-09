require('global-jsdom')();
import { Test, TestSuite } from "xunit.ts";
import FilterForm from '../App/filter-form.vue';
import { shallowMount as mount } from '@vue/test-utils';
import Reading from "../App/Reading";
import Point from "../App/Point";
import Signal from "../App/Signal";

export default class FilterFormTest extends TestSuite {

    private static signals = [
        new Signal('mac2', 'ssid2', 2, -30),
        new Signal('mac5', 'ssid2', 2, -50),
        new Signal('mac3', 'ssid2', 5, -30),
        new Signal('mac4', 'ssid2', 5, -60),
        new Signal('mac1', 'ssid1', 2, -40),
        new Signal('mac7', 'ssid1', 5, -70),
        new Signal('mac6', 'ssid1', 5, -90),
        new Signal('mac8', 'ssid1', 2, -80),
    ];

    @Test()
    async accessPointsAreSortedWhenGroupedBySSIDAndFrequency() {
        //arrange
        const component = mount(FilterForm, { propsData: { current: new Reading(0, new Point(0, 0), FilterFormTest.signals) } });

        //act
        const options = component.findAll('option');

        //assert
        this.assert.equal('ssid1', options.at(0).text());
        this.assert.equal('ssid2', options.at(1).text());
    }

    @Test()
    async accessPointsAreSortedWhenGroupedBySSID() {
        //arrange
        const component = mount(FilterForm, { propsData: { current: new Reading(0, new Point(0, 0), FilterFormTest.signals) } });

        //act
        component.get('#group-by-frequency').setChecked(false);
        await component.vm.$nextTick();

        //assert
        const options = component.findAll('option');
        this.assert.equal('ssid1 @ 2 GHz', options.at(0).text());
        this.assert.equal('ssid1 @ 5 GHz', options.at(1).text());
        this.assert.equal('ssid2 @ 2 GHz', options.at(2).text());
        this.assert.equal('ssid2 @ 5 GHz', options.at(3).text());
    }

    @Test()
    async accessPointsAreSortedWhenNotGrouped() {
        //arrange
        const component = mount(FilterForm, { propsData: { current: new Reading(0, new Point(0, 0), FilterFormTest.signals) } });

        //act
        component.get('#group-by-ssid').setChecked(false);
        await component.vm.$nextTick();

        //assert
        const options = component.findAll('option');
        this.assert.equal('ssid1 @ 2 GHz (mac1)', options.at(0).text());
        this.assert.equal('ssid1 @ 2 GHz (mac8)', options.at(1).text());
        this.assert.equal('ssid1 @ 5 GHz (mac6)', options.at(2).text());
        this.assert.equal('ssid1 @ 5 GHz (mac7)', options.at(3).text());
        this.assert.equal('ssid2 @ 2 GHz (mac2)', options.at(4).text());
        this.assert.equal('ssid2 @ 2 GHz (mac5)', options.at(5).text());
        this.assert.equal('ssid2 @ 5 GHz (mac3)', options.at(6).text());
        this.assert.equal('ssid2 @ 5 GHz (mac4)', options.at(7).text());
    }

    @Test()
    async selectingAccessPointEmitsEvent() {
        //arrange
        const component = mount(FilterForm, { propsData: { current: new Reading(0, new Point(0, 0), FilterFormTest.signals) } });

        //act
        component.get('option:first-child').setSelected();

        //assert
        this.assert.notNull(component.emitted('selected'));
    }

    @Test()
    async groupBySSIDEnablesGroupByFrequency() {
        //arrange
        const component = mount(FilterForm, { propsData: { current: new Reading(0, new Point(0, 0), []) } });

        //act
        component.get('#group-by-ssid').setChecked(true);
        await component.vm.$nextTick();

        //assert
        const checkbox = component.get('#group-by-frequency');
        this.assert.null(checkbox.attributes("disabled"));
    }

    @Test()
    async disablingGroupBySSIDDisablesAndUnchecksGroupByFrequency() {
        //arrange
        const component = mount(FilterForm, { propsData: { current: new Reading(0, new Point(0, 0), []) } });

        //act
        component.get('#group-by-ssid').setChecked(false);
        await component.vm.$nextTick();

        //assert
        const checkbox = component.get('#group-by-frequency');
        this.assert.notNull(checkbox.attributes("disabled"));
        this.assert.null(checkbox.attributes("checked"));
    }

    @Test()
    async selectingBackgroundFileEmitsEvent() {
        //arrange
        const component = mount(FilterForm, { propsData: { current: new Reading(0, new Point(0, 0), []) } });

        //act
        await component.get('#background-file').trigger('change');

        //assert
        this.assert.notNull(component.emitted('background'));
    }

    @Test()
    async pixelateCheckboxEmitsEvent() {
        //arrange
        const component = mount(FilterForm, { propsData: { current: new Reading(0, new Point(0, 0), []) } });

        //act
        await component.get('#pixelate').trigger('change');

        //assert
        this.assert.notNull(component.emitted('pixelate'));
    }
}