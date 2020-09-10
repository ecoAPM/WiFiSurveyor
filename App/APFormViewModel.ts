import AccessPoint from "./AccessPoint";
import AccessPointGrouping from "./AccessPointGrouping";

export default class APFormViewModel {
  selected: AccessPoint | null = null;
  group_by: AccessPointGrouping = new AccessPointGrouping();
}