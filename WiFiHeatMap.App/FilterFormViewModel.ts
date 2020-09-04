import AccessPoint from "./AccessPoint";
import AccessPointGrouping from "./AccessPointGrouping";

export default class FilterFormViewModel {
  group_by: AccessPointGrouping = new AccessPointGrouping();
  selected: AccessPoint | null = null;
  pixelated: boolean = false;
}