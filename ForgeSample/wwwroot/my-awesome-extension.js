function MyAwesomeExtension(viewer, options) {
    Autodesk.Viewing.Extension.call(this, viewer, options);
  }
  
  MyAwesomeExtension.prototype = Object.create(Autodesk.Viewing.Extension.prototype);
  MyAwesomeExtension.prototype.constructor = MyAwesomeExtension;
  
  MyAwesomeExtension.prototype.load = function() {
    var viewer = this.viewer;

    var lockBtn = document.getElementById('MyAwesomeLockButton');
    lockBtn.addEventListener('click', function() {
      viewer.setNavigationLock(true);
    });
  
    var unlockBtn = document.getElementById('MyAwesomeUnlockButton');
    unlockBtn.addEventListener('click', function() {
      viewer.setNavigationLock(false);
    });
  
    return true;
  };
  
  MyAwesomeExtension.prototype.unload = function() {
    alert('MyAwesomeExtension is now unloaded!');
    return true;
  };

  MyAwesomeExtension.prototype.lockViewport = function() {
    this.viewer.setNavigationLock(true);
  };
  
  MyAwesomeExtension.prototype.unlockViewport = function() {
    this.viewer.setNavigationLock(false);
  };
  
  Autodesk.Viewing.theExtensionManager.registerExtension('MyAwesomeExtension', MyAwesomeExtension);