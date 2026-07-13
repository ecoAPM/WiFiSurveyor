import { shallowMount, VueWrapper } from "@vue/test-utils";
import { ComponentPublicInstance, DefineComponent, nextTick } from "vue";

import AppViewModel from "../App/AppViewModel";

export type Component = VueWrapper<unknown, ComponentInstance>;
export type ComponentInstance = ComponentPublicInstance<unknown, { state: AppViewModel }>;
export type ComponentDefinition = DefineComponent<ComponentInstance>;

export async function mount(component: ComponentDefinition, state: AppViewModel) {
	const wrapper = shallowMount(component);
	await setState(wrapper, state);
	return wrapper;
}

async function setState(wrapper: Component, state: AppViewModel) {
	wrapper.vm.state = state;
	await nextTick();
}