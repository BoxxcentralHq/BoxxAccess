import { HugeiconsIcon } from "@hugeicons/react";
import { Settings02Icon, UserCircleIcon } from "@hugeicons/core-free-icons";
import { SidebarTrigger } from "@/components/ui/sidebar";
import { Separator } from "@/components/ui/separator";
import { Button } from "@/components/ui/button";

export default function SiteHeader() {
  return (
    <header className="sticky top-0 z-20 flex h-(--header-height) shrink-0 items-center justify-between border-b border-border bg-background px-4">
      <div className="flex items-center gap-2">
        <SidebarTrigger />
        <Separator orientation="vertical" className="h-5" />
        <span className="text-lg font-semibold tracking-tight text-foreground">
          Boxx<span className="text-primary">Access</span>
        </span>
      </div>

      <div className="flex items-center gap-2">
        <Button variant="ghost" size="icon" aria-label="Settings">
          <HugeiconsIcon icon={Settings02Icon} size={20} />
        </Button>
        <Separator orientation="vertical" className="h-5" />
        <div className="flex items-center gap-2 px-1">
          <HugeiconsIcon icon={UserCircleIcon} size={24} />
          <span className="text-sm font-medium text-foreground">admin</span>
        </div>
      </div>
    </header>
  );
}
