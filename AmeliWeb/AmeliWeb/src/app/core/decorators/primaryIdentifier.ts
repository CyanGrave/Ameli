import { BaseEntity } from '../../core/models/baseEntity';

export function primaryIdentifier(this: any, target: BaseEntity, key: string) {

  if (!target.idProperties.has(key)) {
    target.idProperties.set(key, null);
  }



  // property value
  var _val = this[key];

  // property getter
  var getter = function () {
    return _val;
  };

  // property setter
  var setter = function (newVal: any) {
    target.idProperties.set(key, newVal);
    _val = newVal;
  };

  // Delete property.
  if (delete this[key]) {

    // Create new property with getter and setter
    Object.defineProperty(target, key, {
      get: getter,
      set: setter,
      enumerable: true,
      configurable: true
    });
  }
}
