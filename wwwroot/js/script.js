window.isMobile = function () {
  if (navigator.userAgentData) {
    return navigator.userAgentData.mobile;
  }
  return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(
    navigator.userAgent
  );
};
