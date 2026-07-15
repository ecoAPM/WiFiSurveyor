import { shallowMount, VueWrapper } from "@vue/test-utils";
import { ComponentPublicInstance, DefineComponent, nextTick } from "vue";

import AppViewModel from "../App/AppViewModel";

export type Component = VueWrapper<unknown, ComponentInstance>;
export type ComponentInstance = ComponentPublicInstance<object, { state: AppViewModel }>;
export type ComponentDefinition = DefineComponent<ComponentInstance>;

export async function mount(component: ComponentDefinition, state: AppViewModel = new AppViewModel(), props: object = {}) {
	const wrapper = shallowMount(component, { props: props });
	await setState(wrapper, state);
	return wrapper;
}

async function setState(wrapper: Component, state: AppViewModel) {
	wrapper.vm.state = state;
	await nextTick();
}